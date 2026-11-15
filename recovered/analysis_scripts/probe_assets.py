import sys,pathlib,zipfile,collections,json,re
sys.path.insert(0,str(pathlib.Path('work/pylibs').resolve()))
import UnityPy
out=pathlib.Path('outputs/MoleKart-Reverse'); results={}
for kind,path in [('apk',r'D:\BaiduNetdiskDownload\摩尔卡丁车1 1.1.1.apk'),('ipa',r'D:\BaiduNetdiskDownload\摩尔卡丁车1 1.1.1.ipa')]:
 z=zipfile.ZipFile(path);ns=[n for n in z.namelist() if n.endswith('resources.assets') or n.endswith('sharedassets0.assets') or n.endswith('/mainData') or n.endswith('/level0')]
 for n in ns:
  print('TRY',kind,n)
  try:
   e=UnityPy.load(z.read(n));cnt=collections.Counter(o.type.name for o in e.objects)
   print('PARSED',len(e.objects),dict(cnt)); results[kind+':'+n]={'types':dict(cnt),'files':[{ 'version':str(getattr(f,'unity_version',None)), 'format':getattr(f,'version',None)} for f in e.files.values()]}
   dest=out/kind/'asset_samples';dest.mkdir(exist_ok=True)
   done=0
   for obj in e.objects:
    if obj.type.name in ['Texture2D','TextAsset'] and done<5:
     try:
      data=obj.read(); name=re.sub(r'[^\w.-]','_',data.m_Name)[:90]
      if obj.type.name=='Texture2D': data.image.save(dest/(name+f'_{obj.path_id}.png'))
      else:(dest/(name+f'_{obj.path_id}.txt')).write_bytes(data.m_Script.encode('utf8','surrogateescape') if isinstance(data.m_Script,str) else data.m_Script)
      done+=1
     except Exception as ex:print('EXPORT_ERROR',str(ex)[:200])
  except Exception as ex:print('ERROR',type(ex).__name__,str(ex)[:400]);results[kind+':'+n]={'error':str(ex)}
(out/'asset_probe.json').write_text(json.dumps(results,ensure_ascii=False,indent=2),encoding='utf8')
