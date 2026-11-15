import zipfile,plistlib,struct,json
p=r'D:\BaiduNetdiskDownload\摩尔卡丁车1 1.1.1.ipa'
z=zipfile.ZipFile(p)
infos=[n for n in z.namelist() if n.startswith('Payload/') and n.count('/')==2 and n.endswith('/Info.plist')]
for n in infos:
 d=plistlib.loads(z.read(n)); root=n.rsplit('/',1)[0]+'/'
 print('INFO',json.dumps({k:d.get(k) for k in ['CFBundleDisplayName','CFBundleIdentifier','CFBundleShortVersionString','CFBundleVersion','CFBundleExecutable','MinimumOSVersion','DTSDKName','DTPlatformVersion','UIRequiredDeviceCapabilities','UIDeviceFamily']},ensure_ascii=False))
 b=z.read(root+d['CFBundleExecutable'])
 print('EXECUTABLE_BYTES',len(b),'MAGIC',b[:4].hex())
 slices=[]
 if b[:4]==b'\xca\xfe\xba\xbe':
  count=struct.unpack_from('>I',b,4)[0]
  for i in range(count):
   cpu,sub,off,size,align=struct.unpack_from('>IIIII',b,8+20*i); slices.append((off,size))
 else:slices=[(0,len(b))]
 for off,size in slices:
  magic=b[off:off+4]; endian='<' if magic in [b'\xce\xfa\xed\xfe',b'\xcf\xfa\xed\xfe'] else '>'
  is64=magic in [b'\xcf\xfa\xed\xfe',b'\xfe\xed\xfa\xcf']
  head=struct.unpack_from(endian+'7I',b,off); cpu,sub,ncmds=head[1],head[2],head[4]
  print('ARCH', {12:'arm',16777228:'arm64',7:'x86',16777223:'x86_64'}.get(cpu,cpu),'subtype',sub,'64bit',is64)
  cur=off+(32 if is64 else 28); libs=[]
  for _ in range(ncmds):
   cmd,cs=struct.unpack_from(endian+'II',b,cur)
   if cs<8:break
   if cmd in [12,24,0x80000018,0x8000001f,0x20,0x80000023]:
    so=struct.unpack_from(endian+'I',b,cur+8)[0];libs.append(b[cur+so:cur+cs].split(b'\0')[0].decode('utf8','replace'))
   if cmd in [0x21,0x2c]:print('ENCRYPTION',struct.unpack_from(endian+'III',b,cur+8))
   if cmd==0x25:print('MIN_IOS_PACKED',struct.unpack_from(endian+'II',b,cur+8))
   cur+=cs
  print('LIBRARIES',json.dumps(libs,ensure_ascii=False))
 print('SIGNATURE_FILES',[x for x in z.namelist() if x.startswith(root) and ('CodeResources' in x or x.endswith('embedded.mobileprovision') or x.endswith('SC_Info/') or x.endswith('.sinf'))])
 print('BUNDLED_FRAMEWORKS',[x for x in z.namelist() if '.framework/' in x and x.endswith('Info.plist')])
