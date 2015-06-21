本节笔记介绍变量有关的高级内容

## 变量高级内容 ##

#### 一、类型转换

###### 1.1 隐式转换

隐式转换不需要做任何工作，也不需要另外编写转换代码。如ushort和char位存储完全相同，我们就可以在他们之间进行隐式转换。如下面的例子：

	ushort destinationVar;
	char sourceVar = 'a';
	destinationVar = sourceVar;
	Console.WriteLine("sourceVar val: {0}", sourceVar);
	Console.WriteLine("destinationVar var: {0}", destinationVar);

这段代码运行后，会输入以下的结果

	sourceVar val： a
	destinationVar val: 97


###### 1.2 显式转换

今天看的关于数据类型转换的内容明早更新，今天内容没有了。