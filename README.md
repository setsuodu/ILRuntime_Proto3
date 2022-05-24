# ILRuntime_Proto3

这个是ILRuntime整合Proto3的U3D示例工程

## 版本

- [![unity](https://img.shields.io/badge/Unity-2020.3.33f1c2-blue.svg?logo=unity)]()
- [![vs](https://img.shields.io/badge/VisualStudio-2022-purple.svg?logo=visualstudio)]()
- [![pb](https://img.shields.io/badge/Protobuf-2.4.0.(支持Repeated)-yellow.svg?logo=google)]()
- [![pb](https://img.shields.io/badge/WampServer-3.2.6-blue.svg?logo=data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAAGXcA1uAAAACXBIWXMAAAsTAAALEwEAmpwYAAAFwmlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDIgNzkuMTYwOTI0LCAyMDE3LzA3LzEzLTAxOjA2OjM5ICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ0MgKFdpbmRvd3MpIiB4bXA6Q3JlYXRlRGF0ZT0iMjAyMi0wNC0zMFQwMTo1Mzo0MiswODowMCIgeG1wOk1vZGlmeURhdGU9IjIwMjItMDQtMzBUMDE6NTg6NTcrMDg6MDAiIHhtcDpNZXRhZGF0YURhdGU9IjIwMjItMDQtMzBUMDE6NTg6NTcrMDg6MDAiIGRjOmZvcm1hdD0iaW1hZ2UvcG5nIiBwaG90b3Nob3A6Q29sb3JNb2RlPSIzIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjM3ZTZlNmY1LWQxOTAtMzg0NC1hYzgzLTRjNTZkZmZmZmM5NSIgeG1wTU06RG9jdW1lbnRJRD0iYWRv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)]()
- .NetCore 3.1.0
- ILRuntime 2.0.2

## 使用步骤

1. 拉取本工程完整代码，注意包含一个服务器的submodule工程；
2. 打开服务器项目``\NetCoreServer\NetCoreApp.sln``（或在Unity中启动该项目 ``菜单栏/Assets/Open Server Project``），生成解决方案；
3. 打开Unity项目，``菜单栏/Assets/Open C# Project``；选择``HotFix_Project``，右键生成，生成路径配置在``Unity\Assets\StreamingAssets``；Unity菜单中找到``Tools/热更新/MoveDll``，将热更新编译出的文件移动到``Bundles``包目录下，ILR代码将作为AB打包；
4. Tools/打包AB/StandaloneWindows64，打包资源；打包完在Unity Console中可以看到输出目录，拷贝到服务器上；
5. 启动一个web服务，IIS，Nignx，Apache等等，部署AB包；
目录结构如下：
```
www\download\
www\download\StandaloneWindows64\
www\download\Android\
www\download\iOS\
www\download\present.json
```
6. Unity中通过设置宏定义 ``USE_ASSETBUNDLE``，可在Unity中走AB包，或者默认走Editor资源加载；
7. 运行场景Init；

## 其他

1. 热更工程（HotFix_Project）配置：
   - 引用``Unity\Library\ScriptAssemblies``中的Assembly Definition（UnityScript.dll, UnityThirdParty.dll）。
   - 新版Untiy的引擎dll是根据功能分开编译的，如有需要用到声音，动画等，复制一份放到 \Libs\ 下，引用；
   - 引用中所有dll设置为不输出。
2. Proto打包。
   - 源文件是 \Protoc\OuterMessage.proto；
   - 打包使用 ``Tools/生成Proto``，会分别生成到热更工程和服务器工程中；
