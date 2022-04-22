# ILRuntime_Proto3
Unity3D demo project for ILRuntime

这个是ILRuntime整合Proto3的U3D示例工程

### 版本
- Unity2020.3.30f1c1
- VS2019 Professional
- Proto 3.0.0-win32 （已支持Repeated）
- ILRuntime 2.0.2
- wampserver 3.2.6
- .NetCore 3.1.0

### 环境配置
- 1. ``'devenv' 不是内部或外部命令``。系统环境变量\Path\中，添加devenv.exe的目录。
- 2. 下载 [http://protoc-3.x.x-win64.zip](https://github.com/protocolbuffers/protobuf/releases)，解压将其中的protoc.exe拷贝到 ``C:\windows\system32``，使用命令测试 proto环境 ``protoc --version``。
- 3. 运行 ``.\ILRuntimeQ\Protoc\run.bat``，将 *.proto 转化为 *.cs。
- 4. Web服务器配置：
```
.\download
.\download\StandaloneWindows64
.\download\present.json
```

- 5. 热更工程配置：
   1. 引用``Unity\Library\ScriptAssemblies``中的Assembly Definition。
   2. 引用中设置不输出的dll。

### 使用说明

- 1. HotFix工程已嵌入到Untiy工程中，手动编译HotFix解决方案，生成 ``HotFix.dll ``到 ``StreamingAssets`` 目录下。
- 2. 使用工具栏 ``Tools/打包AB/MoveDLL`` 将 HotFix.dll 热更新程序集拷贝到 AssetBundle 资源打包目录中。
- 3. 再打包AB，将热更新代码座位AB打包。
- 4. 注意阿里云、腾讯云等远程部署时，端口规则不在远程电脑上设置，而是在平台防火墙规则管理中。

### //TODO
- [x] 1. 使用包管理或其他方法，方便升级 ILRuntime 源码。
- [ ] 2. 打包热更新代码再自动化一点。允许打包单个资源。
- [ ] 3. 参考[ET初始化](https://github.com/egametang/ET/blob/9326784f37d5eacfda469c93b2764d18f3c6f101/Unity/Assets/Mono/CodeLoader.cs)，把可能的委托类型都注册了占位，避免需要时无法热更。

