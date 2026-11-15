import sys,pathlib,zipfile,json,collections,hashlib
sys.path.insert(0,str(pathlib.Path('work/pylibs').resolve()))
import UnityPy
base=pathlib.Path('outputs/MoleKart-Reverse')
for kind,path in [('apk',r'D:\BaiduNetdiskDownload\摩尔卡丁车1 1.1.1.apk'),('ipa',r'D:\BaiduNetdiskDownload\摩尔卡丁车1 1.1.1.ipa')]:
 z=zipfile.ZipFile(path);count=0
 for ent in json.loads((base/kind/'asset_manifest.json').read_text(encoding='utf8'))['files']:
  name=ent['file'];n=next(n for n in z.namelist() if n.endswith('/'+name));e=UnityPy.load(z.read(n));dest=base/kind/'RecoveredAssets'/name/'raw_scripts'
  for obj in e.objects:
   if obj.type.name=='MonoBehaviour':
    dest.mkdir(exist_ok=True);(dest/(str(obj.path_id)+'.bin')).write_bytes(obj.get_raw_data());count+=1
 print(kind,'raw script components',count)
