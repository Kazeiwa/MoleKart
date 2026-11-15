import zipfile,collections,re,json,struct,pathlib
out=pathlib.Path('work/reverse');out.mkdir(parents=True,exist_ok=True)
for kind,path in [('apk',r'D:\BaiduNetdiskDownload\摩尔卡丁车1 1.1.1.apk'),('ipa',r'D:\BaiduNetdiskDownload\摩尔卡丁车1 1.1.1.ipa')]:
 z=zipfile.ZipFile(path);ns=z.namelist()
 (out/(kind+'_inventory.txt')).write_text('\n'.join(f'{i.file_size:10} {i.filename}' for i in z.infolist()),encoding='utf8')
 print(kind,'entries',len(ns),'extensions',collections.Counter(pathlib.PurePosixPath(n).suffix.lower() for n in ns).most_common(25))
 print('CODE',[n for n in ns if n.endswith(('.so','.dex','.js','.lua','.luac','.dll'))])
 print('TOP',[(i.filename,i.file_size) for i in sorted(z.infolist(),key=lambda i:i.file_size,reverse=True)[:15]])
 if kind=='apk':
  for n in ns:
   if n.endswith(('.so','.dex')) or n=='AndroidManifest.xml':
    b=z.read(n); (out/pathlib.PurePosixPath(n).name).write_bytes(b)
    ss=[s.decode('ascii') for s in re.findall(rb'[\x20-\x7e]{6,}',b)]
    (out/(pathlib.PurePosixPath(n).name+'.strings.txt')).write_text('\n'.join(ss),encoding='utf8')
    if n.endswith('.so'):
     print('ELF',n,b[:20].hex())
     print('ENGINE_STRINGS',[s[:220] for s in ss if re.search(r'cocos|unity|irrlicht|ogre|bullet|marmalade|cocos2d|version|JNI_OnLoad',s,re.I)][:45])
