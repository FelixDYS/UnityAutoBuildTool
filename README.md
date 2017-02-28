## UnityAutoBuildTool
###简介
```
本工具根据个人工作需求编写，其中的Editor编辑器方面是根据工作内容进行的的编写，使用者可根据自己需求编写。
代码可在4.x和5.版本下使用，但根据本人使用情况，在5.3.2左右的版本中Unity将UniEdito.iOS.Xcode该命名空间隐藏，导致在压Xcode工程时，动态库X.tbd无法自动添
本工程的分享，主要目的是在于将我使用的工具分享，也是避免自己忘记，其中很多是适合我当前项目工程的，后续会做出整理，采用XML文件作为配置内容，进行自动压包，开发通用工具，有需求的朋友可以留言交流。
```

###核心介绍
```
核心代码就是BL_BuildPostProcess.cs文件，其中包含了自动添加动态库，自动修改证书，自动修改代码（xcode代码），自动修改文件，自动修改Plist文件等功能，
拓展简单，使用者都可以自己编辑，其中XClass.cs文件是根据雨凇MOMO的提供的源码添加进的，主要用于修改xcode代码。
```

###截图介绍
![BuildTool_1](http://oex3qda2c.bkt.clouddn.com/BuildTool_1.png)
![BuildTool_2](http://oex3qda2c.bkt.clouddn.com/BuildTool_2.png)
###操作演示
![BuildTool_3](http://oex3qda2c.bkt.clouddn.com/BuildTool_3.gif)
![BuildTool_4](http://oex3qda2c.bkt.clouddn.com/BuildTool_4.gif)
