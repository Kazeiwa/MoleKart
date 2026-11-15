import pathlib,gzip,json,struct,re,collections
base=pathlib.Path('outputs/MoleKart-Reverse')
for kind in ['apk','ipa']:
 stats=collections.Counter();errors=[]
 for p in (base/kind/'RecoveredAssets').glob('*/objects.jsonl.gz'):
  with gzip.open(p,'rt',encoding='utf8') as f:
   for line in f:
    o=json.loads(line);d=o.get('tree',{});typ=o['type']
    if typ not in ['Mesh','AudioClip']:continue
    name=re.sub(r'[^\w.-]','_',d.get('m_Name','unnamed'))[:80];prefix=f"{o['path_id']}_{name}"
    try:
     if typ=='AudioClip':
      b=bytes(d.get('m_AudioData',[]))
      if not b:continue
      ext='.mp3' if b.startswith(b'ID3') or b[:2] in [b'\xff\xfb',b'\xff\xf3',b'\xff\xf2'] else '.wav' if b.startswith(b'RIFF') else '.ogg' if b.startswith(b'OggS') else '.audio.bin'
      (p.parent/(prefix+ext)).write_bytes(b);stats['audio']+=1;continue
     vs=d.get('m_Vertices',[])
     if not vs:raise ValueError('No unpacked vertices; serialized data retained')
     raw=bytes(d['m_IndexBuffer']);unit=2 if d['m_Use16BitIndices'] else 4;inds=struct.unpack('<'+('H' if unit==2 else 'I')*(len(raw)//unit),raw)
     faces=[]
     for sm in d['m_SubMeshes']:
      start=sm['firstByte']//unit;a=inds[start:start+sm['indexCount']]
      if sm.get('isTriStrip',0):
       fs=[(a[i+1],a[i],a[i+2]) if i%2 else (a[i],a[i+1],a[i+2]) for i in range(len(a)-2) if len(set(a[i:i+3]))==3]
      else:fs=[tuple(a[i:i+3]) for i in range(0,len(a)-2,3)]
      if sm.get('triangleCount') is not None and len(fs)!=sm['triangleCount']:raise ValueError(f'Triangle count mismatch {len(fs)} vs {sm["triangleCount"]}')
      faces.append(fs)
     if any(i>=len(vs) for fs in faces for face in fs for i in face):raise ValueError('Out of range vertex index')
     uv=d.get('m_UV',[]);norm=d.get('m_Normals',[]);useuv=len(uv)==len(vs);usenorm=len(norm)==len(vs)
     with (p.parent/(prefix+'.obj')).open('w',encoding='utf8') as out:
      out.write('# Recovered local-space mesh; materials, rig and scene links remain in objects.jsonl.gz\n')
      for v in vs:out.write(f'v {v["x"]} {v["y"]} {v["z"]}\n')
      if useuv:
       for v in uv:out.write(f'vt {v["x"]} {v["y"]}\n')
      if usenorm:
       for v in norm:out.write(f'vn {v["x"]} {v["y"]} {v["z"]}\n')
      for gi,fs in enumerate(faces):
       out.write(f'g submesh_{gi}\n')
       for face in fs:
        refs=[]
        for i in face:
         s=str(i+1);refs.append(s+'/'+(s if useuv else '')+'/'+s if usenorm else s+'/'+s if useuv else s)
        out.write('f '+' '.join(refs)+'\n')
     stats['mesh_obj']+=1
    except Exception as ex:errors.append([str(p.parent.name),o['path_id'],typ,str(ex)])
 (base/kind/'legacy_export.json').write_text(json.dumps({'counts':stats,'errors':errors},ensure_ascii=False,indent=2),encoding='utf8')
 print(kind,dict(stats),'errors',len(errors),errors[:2],flush=True)
