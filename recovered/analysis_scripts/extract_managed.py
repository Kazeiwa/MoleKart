import zipfile,pathlib,hashlib,re,struct
for kind,path in [('apk',r'D:\BaiduNetdiskDownload\摩尔卡丁车1 1.1.1.apk'),('ipa',r'D:\BaiduNetdiskDownload\摩尔卡丁车1 1.1.1.ipa')]:
 z=zipfile.ZipFile(path);out=pathlib.Path('work/reverse')/kind;out.mkdir(exist_ok=True)
 for n in z.namelist():
  if '/Managed/' in n and n.endswith(('.dll','.mdb')):
   b=z.read(n);(out/pathlib.PurePosixPath(n).name).write_bytes(b)
   print(kind,pathlib.PurePosixPath(n).name,len(b),hashlib.sha256(b).hexdigest()[:16])
 for n in z.namelist():
  if n.endswith(('mainData','globalgamemanagers','resources.assets')) or re.search(r'/level\d+$',n):
   b=z.read(n);print('HEADER',kind,n,repr(b[:90]))
