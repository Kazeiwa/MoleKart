import sys,pathlib,zipfile,collections,json,re,gzip,hashlib
sys.path.insert(0,str(pathlib.Path('work/pylibs').resolve()))
import UnityPy
base=pathlib.Path('outputs/MoleKart-Reverse')
for kind,path in [('apk',r'D:\BaiduNetdiskDownload\摩尔卡丁车1 1.1.1.apk'),('ipa',r'D:\BaiduNetdiskDownload\摩尔卡丁车1 1.1.1.ipa')]:
 z=zipfile.ZipFile(path); dest=base/kind/'RecoveredAssets';dest.mkdir(exist_ok=True)
 summary={'files':[],'types':collections.Counter(),'exports':collections.Counter(),'errors':[]}
 for n in z.namelist():
  if not(n.endswith('.assets') or n.endswith('/mainData') or re.search(r'/level\d+$',n)):continue
  stem=pathlib.PurePosixPath(n).name
  try:e=UnityPy.load(z.read(n))
  except Exception as ex:summary['errors'].append([n,'parse',str(ex)]);continue
  entry={'file':stem,'objects':len(e.objects),'versions':[str(getattr(f,'unity_version','')) for f in e.files.values()],'externals':[]}
  for f in e.files.values():
   for ref in getattr(f,'externals',[]):entry['externals'].append(str(getattr(ref,'path','')))
  summary['files'].append(entry)
  target=dest/stem;target.mkdir(exist_ok=True)
  with gzip.open(target/'objects.jsonl.gz','wt',encoding='utf8') as treeout:
   for obj in e.objects:
    typ=obj.type.name;summary['types'][typ]+=1
    rec={'path_id':obj.path_id,'type':typ}
    try:rec['tree']=obj.read_typetree()
    except Exception as ex:rec['tree_error']=str(ex)
    treeout.write(json.dumps(rec,ensure_ascii=True,default=lambda o: {'bytes_hex':o.hex()} if isinstance(o,bytes) else str(o))+'\n')
    if typ not in ('Texture2D','Mesh','TextAsset','AudioClip'):continue
    try:
     d=obj.read();name=re.sub(r'[^\w.-]','_',getattr(d,'m_Name','unnamed'))[:80];prefix=f'{obj.path_id}_{name}'
     if typ=='Texture2D':d.image.save(target/(prefix+'.png'))
     elif typ=='Mesh':(target/(prefix+'.obj')).write_text(d.export(),encoding='utf8')
     elif typ=='TextAsset':
      s=d.m_Script;(target/(prefix+'.txt')).write_bytes(s.encode('utf8','surrogateescape') if isinstance(s,str) else s)
     else:
      # Preserve original audio bytes; codec/container is not presumed.
      data=getattr(d,'m_AudioData',None)
      if not data:continue
      (target/(prefix+'.audio.bin')).write_bytes(data)
     summary['exports'][typ]+=1
    except Exception as ex:summary['errors'].append([stem,obj.path_id,typ,str(ex)[:220]])
  print(kind,stem,len(e.objects),flush=True)
 (base/kind/'asset_manifest.json').write_text(json.dumps(summary,ensure_ascii=False,indent=2),encoding='utf8')
 print('DONE',kind,dict(summary['types']),dict(summary['exports']),'errors',len(summary['errors']),flush=True)
