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

###### 2.3 数组

前面介绍的变量类型都有共同之处，就是它们只存储一个值或一组值。有时需要存储很多数据，就会出现不便，这时需要的是同时存储几个类型相同的值，而不是每个值使用不同变量。这样有两种解决方案，一种就是一个数据一个数据列出来，另一种就是使用数组。数组是一个变量的索引列表，存储在数组类型的变量中。在方括号中指定索引，即可访问该数组中的成员。

这个索引是一个整数，第一个条目的索引是0，第二个条目的索引是1，以此类推。这样我们可以使用循环遍历所有元素，例如：

	int i;
	for (i = 0; i < 3; i++)
	{
		Console.WriteLine("Name with index of {0}: {1}", i, friendNames[i]);
	}

【声明数组】

数组通过下述的方式声明：

	<baseType>[] <name>;

数组必须在访问之前初始化，不能直接写一个方括号就给数组元素赋值或使用数组。数组的初始化方式有两种。可以字面形式指定数组的完整内容，也可以指定数组的大小，再使用关键字new初始化所有数组元素。

1）使用字面值初始化数组，只需要提供一个逗号分隔元素值列表，该列表放在花括号中，例如：

	int[] myIntArray = {5, 9, 10, 2, 99};

2)使用关键字new显式的初始化数组，用一个常量值定义其大小。这种方法会给所有的数组元素赋予同一个默认值，对于数值类型来说，其默认值是0.也可以使用非常量的变量进行初始化，例如：

    int[] myIntArray = new int[arraySize];

还可以使用两种初始化方式的组合：

	int[] myIntArray = new int[5] {5, 9, 10, 2, 99};

这种方式需要注意数组大小与元素个数相匹配。如果使用变量定义数组大小，该变量必须定义为常量，如：

	const int arraySize = 5;
	int[] myIntArray = new int[arraySize] {5, 9, 10, 2, 99};

数组的声明和初始化可以分开进行，像下面这样就是合法的：

	int[] myIntArray;
	myIntArray = new int[5];

【foreach循环】

foreach循环可以使用一种简便的语法来定位数组中的每个元素：

	foreach (<baseType> <name> in <array>)
	{
		//can use <name> for each element
	}

这个循环会迭代每个元素，依次把元素放在变量<name>中，且不存在访问非法元素的危险。不需要考虑数组中有多少元素，并可以确保将在循环中使用每个元素。使用这个循环，我们改写本节的实例：

	static void Main(string[] args)
	{
		string[] friendNames = {"Robert Barwell", "Mike Parry", "Jeremy Beacock"};
		Console.WriteLine("这是我的{0}位朋友：", friendNames.Length);

		foreach (string friendName in friendNames)
		{
			Console.WriteLine(friendName);
		}

		Console.ReadKey();
	}

使用foreach方法和标准的for循环方法主要区别在于： foreach循环对数组内容进行的是只读访问，所以不能改变任何元素的值。也就是其中不能对任何元素进行重新赋值，如果编译，会造成失败。

【多维数组】

多维数组是使用多个索引访问其元素的数组。像二维数组就可以这样声明： `<baseType>[,] <name>;`

多维数组只需要更多的逗号，如`<baseType>[,,,] <name>`这样就声明了一个四维数组。赋值类似，用逗号分隔开。

foreach可以访问多维数组中的所有元素，方式与一维数组访问相同，例如：

	double[,] hillHeight = {{1,2,3,4},{2,3,4,5},{3,4,5,6}};
	foreach (double height in hillHeight)
	{
		Console.WriteLine("{0}",height);
	}

元素的输出顺序与赋予字面值的顺序相同（这些显示了元素的标示符，而不是实际值）：

	hillHeight[0,0]
	hillHeight[0,1]
	hillHeight[0,2]
	hillHeight[0,3]
	hillHeight[1,0]
	hillHeight[1,1]
	hillHeight[1,2]

【数组的数组】

上面的多维数组也可称为矩形数组，这是因为每行元素个数都相等。也可以使用锯齿数组（jagged array），其中每行都有不同的元素个数。为了达到复杂的数组，我们会使用其中的元素是另外的数组。也就是数组的数组。声明数组的数组，语法要在数组的声明中指定多个方括号对，例如：

	int[][] jaggedIntArray;

初始化不能使用前面的方式，这里有两种方式：

可以初始化包含其他数组的数组，称之为子数组，然后依次初始化子数组：

	jaggedIntArray = new int[2][];
	jaggedIntArray[0] = new int[3];
	jaggedIntArray[1] = new int[4];

也可以使用字面值赋值的改进形式：

	jaggedIntArray = new int[3][] {new int[] { 1, 2, 3 }, new int[] { 1 }, new int[] { 1, 2 }};

对锯齿数组可以使用foreach循环，但必须使用嵌套才行。假定下述锯齿数组包含10个数组，每个数组又包含一个整数数组，其元素是1~10的约数：

	int[][] divisors1To10 = {new int[] {1},
							 new int[] {1, 2},
							 new int[] {1, 3},
							 new int[] {1, 2, 4},
							 new int[] {1, 5},
							 new int[] {1, 2, 3, 6},
							 new int[] {1, 7},
							 new int[] {1, 2, 4, 8},
							 new int[] {1, 3, 9},
							 new int[] {1, 2, 5, 10} };

下面的代码会失效：

	foreach (int divisor in divisors1To10)
	{
		Console.WriteLine(divisor);
	}

这是因为数组包含的元素int[]，而不是int，必须循环每个子数组和数组本身：

	foreach (int[] divisorsOfInt in divisors1To10)
	{
		foreach (int divisor in divisorsOfInt)
		{
			Console.WriteLine(divisor);
		}
	}

#### 三、字符串的处理

string类型的变量可看做是char变量的只读数组。这样，就可以使用下列语法访问每个字符：

	string mystring = "A string";
	char myChar = mystring[1];

但是不能使用这种方式给各个字符赋值。为了获得一个可写char数组，可以这样写，使用ToCharArray()命令：

	string myString = "A string";
	char[] myChars = myString.ToCharArray();

另外两个常用字符串命令：`<string>.ToLower()` 和 `<string>.ToUpper()`.它们可以分别把字符串转换为代写或小写形式。

如果需要删除输入字符串中的空格，此时可以使用`<string>.Trim()`命令来处理。

	char[] trimChars = {'', 'e', 's'};
	string userResponse = Console.ReadLine();
	userResponse = userResponse.Trim(trimChars);

这样将会从字符串前面后面删除所有的空格、字母e、字母s。

还可以使用`<string>.TrimStart()`和`<string>.TrimEnd()`命令。它们可以把字符串的前面和后面的空格删掉。

还有两个命令`<string>.PadLeft()`和`<string>.PadRight()`。它们可以在字符串左边或右边添加空格，使字符串达到指定的长度。其语法如下：

	<string>.PadX(<desiredLength>);

例如：

	myString = "Aligned";
	myString = myString.PadLeft(10);

提供一个char，而不是像修整命令那样指定一个char数组，可以添加其他字符到字符串前后：

	myString = "Aligned";
	myString = myString.PadLeft(10， '-');

这样将会在myString的开头加上三个短横线。

####Q&A

Q1、下面的转换哪些不是隐式转换：

- a.int转换为short
- b.short转换为int
- c.bool转换为string
- d.byte转换为float

Answer：int范围比short范围大，a不是隐式转换，b是隐式转换；bool值和string值都不是数值范围，互相转换都不是隐式转换，c不是隐式转换；byte没有float范围大，d是隐式转换。

Q2、基于short类型的color枚举包含彩虹的七种颜色，再加上黑色和白色，据此编写color枚举的代码。这个枚举可以使用byte类型吗？

Answer：题面中包含了九种颜色，类型byte含有256个数值，可以枚举256项，即使用byte可以枚举题目的颜色。

	snum color : short
	{
		Red    = 0,
		Orange = 1,
		Yellow = 2,
		Green  = 3,
		Blue   = 4,
		Indigo = 5,
		Violet = 6,
		Black  = 7,
		White  = 8
	}

Q3、下面的代码可以成功编译吗？为什么？

	string[] Blab = new string[5]
	string[5] = 5th string.

Answer:不能成功编译。原因如下：

- 第一行却分号，不是完整的语句；
- 第二行尝试访问的是不存在的第六个元素，第五个元素应为`string[4]`;
- 第二行指定的字符串值没有包含在双引号中。

Q4、编写一个控制台应用程序，它接收用户输入的一个字符串，将其中的字符以与输入相反的顺序输出。

Answer：代码如下

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	
	namespace ConsoleApplication1
	{
	    class Program
	    {
	        static void Main(string[] args)
	        {
	            //颠倒字符串
	            Console.WriteLine("请输入一串字符串：");
	            string aString = Console.ReadLine();
	            string bString = "";     //aString就是aString反转的字符串，这里初始化为空字符串；
	
	            
	            for (int index = aString.Length - 1; index >= 0; index--)
	            {
	                bString += aString[index];
	            }
	            Console.WriteLine("字符串反转后得到的新字符串为{0}", bString);
	            Console.ReadKey();
	        }
	    }
	}

