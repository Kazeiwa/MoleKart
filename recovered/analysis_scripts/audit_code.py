import pathlib,json,re,collections,sys,shutil,xml.etree.ElementTree as ET
sys.path.insert(0,str(pathlib.Path('work/pylibs').resolve()))
import dnfile
base=pathlib.Path('outputs/MoleKart-Reverse');summary={}
for kind in ['apk','ipa']:
 root=base/kind;ref=root/'ReferenceAssemblies';ref.mkdir(exist_ok=True)
 for p in pathlib.Path('work/reverse',kind).glob('*.dll'):shutil.copy2(p,ref/p.name)
 data={}
 for p in root.glob('Assembly-*'):
  if not p.is_dir():continue
  files=list(p.rglob('*.cs'));empty=0;comments=[];lines=0;pinvoke=[]
  for f in files:
   text=f.read_text(encoding='utf-8-sig');lines+=len(text.splitlines());empty+=text.count('Method body consists only')
   if '/*Error:' in text:comments.append(str(f.relative_to(root)))
   if 'DllImport(' in text:pinvoke.append(str(f.relative_to(root)))
  dll=dnfile.dnPE(str(ref/(p.name+'.dll')))
  methods=dll.net.mdtables.MethodDef.rows
  data[p.name]={'cs_files':len(files),'lines':lines,'empty_body_error_count':empty,'files_with_errors':comments,'native_bridge_files':pinvoke,'methods':len(methods),'methods_with_RVA':sum(bool(m.Rva) for m in methods)}
  for proj in p.glob('*.csproj'):
   tree=ET.parse(proj)
   for hint in tree.findall('.//HintPath'):hint.text='../ReferenceAssemblies/'+pathlib.PureWindowsPath(hint.text).name
   tree.write(proj,encoding='unicode',xml_declaration=False)
 summary[kind]=data
apk={str(p.relative_to(base/'apk'/'Assembly-CSharp')) for p in (base/'apk'/'Assembly-CSharp').rglob('*.cs')}
ipa={str(p.relative_to(base/'ipa'/'Assembly-CSharp')) for p in (base/'ipa'/'Assembly-CSharp').rglob('*.cs')}
summary['comparison']={'shared_file_paths':len(apk&ipa),'apk_only':sorted(apk-ipa),'ipa_only':sorted(ipa-apk)}
(base/'code_audit.json').write_text(json.dumps(summary,ensure_ascii=False,indent=2),encoding='utf8')
for k in ['apk','ipa']:
 print(k,{a:{x:y for x,y in v.items() if x not in ['files_with_errors','native_bridge_files']} for a,v in summary[k].items()})
print('shared',len(apk&ipa),'apk only',len(apk-ipa),'ipa only',len(ipa-apk))

