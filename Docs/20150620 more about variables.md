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

隐式转换在很多基本类型中可以进行，我们下面将会列举下来：（这里注意，char存储的是数值，所以char被当做一个数值类型）

		byte      可以安全转换为 short,ushort,int,uint,long,ulong,float,double,decimal
		sbyte     可以安全转换为 short,int,long,float,double,decimal
		short     可以安全转换为 int,long,float,double,decimal
		ushort    可以安全转换为 int,uint,long,ulong,float,double,decimal
		int       可以安全转换为 long,float,double,decimal
		uint      可以安全转换为 long,ulong,float,double,decimal
		long      可以安全转换为 float,double,decimal
		ulong     可以安全转换为 float,double,decimal
		float     可以安全转换为 double
		char      可以安全转换为 ushort,int,uint,long,ulong,float,double,decimal

一般来说不需要专门去记这个列表，只要知道取值范围小的可以直接隐式转换为取值范围大的类型。


###### 1.2 显式转换

在明确要求编译器把数值从一种数据类型转换为另一种数据类型时，就是在执行显示转换。

我们先看看不添加显示转换代码，会出现什么情况：

	byte destinationVar;
	short sourceVar = 7;
	destinationVar = sourceVar;
	Console.WriteLine("sourceVar val: {0}", sourceVar);
	Console.WriteLine("destinationVar val: {0}",destinationVar);

如果编译这段代码，就会产生错误：

	错误	1	无法将类型“short”隐式转换为“byte”。存在一个显式转换(是否缺少强制转换?)
	C:\Users\Administrator\AppData\Local\Temporary Projects\ConsoleApplication1\Program.cs
	14	30	ConsoleApplication1

这里编译器检测出没有进行显式转换，为了编译成功，我们对它进行显式转换。语法很简单`<(destinationType)sourceType>`下面我们对上面的例子进行强制转换：

	byte destinationVar;
	short sourceVar = 7;
	destinationVar = (byte)sourceVar;
	Console.WriteLine("sourceVar val: {0}", sourceVar);
	Console.WriteLine("destinationVar val: {0}",destinationVar);

得到如下结果：

	sourceVar val: 7
	destinationVar val: 7

当试图将一个值转换为不适合的变量时，会发生什么呢？修改代码，如下：

	byte destinationVar;
	short sourceVar = 281;
	destinationVar = (byte)sourceVar;
	Console.WriteLine("sourceVar val: {0}", sourceVar);
	Console.WriteLine("destinationVar val: {0}",destinationVar);

结果如下：

	sourceVar val: 281
	destinationVar val: 25

这是如何造成的？byte最大是255，281 = 100011001，25 = 000011001，我们可以看出，源数据最左一位丢失了。我们对这个问题的处理有两种：一种是检查源数据，看其是否有数据丢失。这个过程很不容易做到。第二种，我们就要判断一个数能不能放入转换的变量中，如果不能就会导致溢出，这就需要检查。

检查我们用到两个关键字checked和unchecked，称为表达式的溢出检查上下文。使用方法：
	
	checked(expression)
	unchecked(expression)

下面对上个例子进行溢出检查：

	byte destinationVar;
	short sourceVar = 281;
	destinationVar = checked((byte)sourceVar);
	Console.WriteLine("sourceVar val: {0}", sourceVar);
	Console.WriteLine("destinationVar val: {0}",destinationVar);

执行代码，会出现下面的错误：

	算术运算导致溢出。

但这段代码中用unchecked代替checked，会得到和以前一样的结果，不会出错。

使用Convert命令进行显式转换：

	Convert.ToBoolean(val);
	Convert.ToByte(val);
	Convert.ToChar(val);
	Convert.ToDecimal(val);
	Convert.ToDouble(val);
	Convert.ToInt16(val);
	Convert.ToInt32(val);
	Convert.ToInt64(val);
	Convert.ToSByte(val);
	Convert.ToSingel(val);  //val转换为float
	Convert.ToUInt16(val);
	Convert.ToUInt32(val);
	Convert.ToUInt64(val);

#### 二、复杂的变量类型

我们这里主要介绍C#中的3个复杂变量类型：枚举、结构和数组。

###### 2.1 枚举

有时候希望变量提取的是一个固定集合的值，这时候就需要使用枚举。枚举需要声明和描述一个用户定义的类型，再声明这个新类型的变量。

【定义枚举】

可以使用enum关键字来定义枚举，如下所示：

	enum <typeName>
	{
		<value1>,
		<value2>,
		<value3>,
		...
		<valueN>
	}

接着声明这个新类型的变量：

	<typeName> <varName>;

并赋值：

	<varName> = <typeName>.<value>;

枚举使用一个基本类型来存储。枚举类型可以提取的每个值都存储为该基本类型的一个值，默认情况下该类型为int。在声明中添加类型，就可以指定其他基本类型：

	enum <typeName> : <underlyingType>
	{
		<value1>,
		<value2>,
		<value3>,
		...
		<valueN>
	}

枚举的基本类型可以是byte、sbyte、short、ushort、int、uint、long和ulong。

在默认情况下，每个值会根据定义的顺序，自动赋给对应的基本类型值。我们这里可以指定这个赋值过程：使用=，并指定每个枚举的实际值：

	enum <typeName> : <underlyingType>
	{
		<value1> = <actualVal1>,
		<value2> = <actualVal2>,
		<value3> = <actualVal3>,
		...
		<valueN> = <actualValN>
	}

还可以使用一个值作为另一个枚举的基础值，为多个枚举指定相同的值：

	enum <typeName> : <underlyingType>
	{
		<value1> = <actualVal1>,
		<value2> = <value1>,
		<value3>,
		...
		<valueN> = <actualValN>
	}

没有赋值的任何值都会自定获得一个初始值，这里使用的值是从比上一个明确声明的值大于1开始的序列。例如上面的代码中，<value3>的值是<value1>+1。

这里赋值可能会造成一些不可预料的错误，具体在赋值过程中需要注意。

###### 2.2 结构

结构就是由几个数据组成的数据结构，这些数据可能具有不同的类型。根据这个结构可以定义自己的变量类型。

【定义结构】

结构是使用struct关键字来定义的，如下所示：

	struct <typeName>
	{
		<memberDeclarations>
	}

<memberDeclarations>包含变量的声明，其格式与往常一样。每个成员的声明都采取如下形式：

	<accessibility> <type> <name>;

例如下面的例子：

	struct route
	{
		public orientation direction;
		public double distance;
	}

定义新的结构之后，就可以定义新类型的变量：

	route myRoute;

还可以通过句点字符访问这个组合变量中的数据成员：

	myRoute.direction = orientation.north;
	myRoute.distance = 2.5;

