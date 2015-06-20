今天我主要阅读的是C#入门经典五版的第四章，流程控制。这里我们主要进行三个方面的笔记，布尔逻辑、分支和循环。

#####布尔逻辑的含义及用法

布尔类型一般用于存储比较结果，需要使用_比较运算符_或叫_关系运算符_。下面就是一些常见的用法：

    1. ==：二元，`var1 = var2 == var3;`如果var2和var3相等，则var1为true，否则为false；
    2. !=：二元，`var1 = var2 != var3;`如果var2不等于var3，则var1为true，否则为false；
    3. < ：二元，`var1 = var2 < var3;`如果var2小于var3，则var1为true，否则为false；
    4. > ：二元，`var1 = var2 > var3;`如果var2大于var3，则var1为true，否则为false；
    5. <=：二元，`var1 = var2 <= var3;`如果var2小于等于var3，则var1为true，否则为false；
    6. >=：二元，`var1 = var2 >= var3;`如果var2大于等于var3，则var1为true，否则为false；
    7. ! ：（逻辑非）一元，`var1 = !var2;`如果var2是false，var1就是true，否则为false；
    8. & ：（逻辑与）二元，`var1 = var2 & var3;`如果var2和var3都是true，var1就是true，否则为false；
    9. | ：（逻辑或）二元，`var1 = var2 | var3;`如果var2或var3是true，var1就是true，否则为false；
    10. ^：（逻辑异或）二元，`var1 = var2 ^ var3;`如果var2或var3中有且只有一个是true，var1的值就是true，否则为false； 
    11. && ：（逻辑与）二元，`var1 = var2 && var3;`如果var2和var3都是true，var1就是true，否则为false；
    12. || ：（逻辑或）二元，`var1 = var2 || var3;`如果var2或var3是true，var1就是true，否则为false；
    13. 第11、12条的与和或与第8、9条相比，结果完全一致，但下面两个的性能较好。

&&和&（||和|）的区别：&&第一个操作数是false，不需要进行第二个操作数，直接返回false；||第一个操作数是true，不需要进行第二个操作数，直接返回true。而&和|，则会总是计算两个操作数。

######布尔赋值运算符：

    1. &=：二元，`var1 &= var2;`var1的值是var1 & var2的值；
    2. |=：二元，`var1 |= var2;`var1的值是var1 | var2的值；
    3. ^=：二元，`var1 ^= var2;`var1的值是var1 ^ var2的值；

布尔赋值运算符和&&、||不同，不论赋值运算符左边的值是多少，都处理所有的操作数。

######按位运算符：

&和|在数值的运算中还能按位进行运算，除了数学应用，其他地方并不常用。

	* 操作数1的位：1   操作数2的位：1   &的结果位：1
	* 操作数1的位：1   操作数2的位：0   &的结果位：0
	* 操作数1的位：0   操作数2的位：1   &的结果位：0
	* 操作数1的位：0   操作数2的位：0   &的结果位：0
	* 操作数1的位：1   操作数2的位：1   |的结果位：1
	* 操作数1的位：1   操作数2的位：0   |的结果位：1
	* 操作数1的位：0   操作数2的位：1   |的结果位：1
	* 操作数1的位：0   操作数2的位：0   |的结果位：0

还有一个一元位运算符~，它可以对操作数中的位取反

	* 操作数的位：1   ~的结果位：0
	* 操作数的位：0   ~的结果位：1

位移运算符：

	1. >>：二元，`var1 = var2 >> var3;`把var2的二进制值向右移动var3位，就得到var1；
	2. <<：二元，`var1 = var2 << var3;`把var2的二进制值向左移动var3位，就得到var1.
	也可以用来赋值
	1. >>=：一元，`var1 >>= var2;`把var1的二进制值向右移动var2位，就得到var1的值；
	2. <<=：一元，`var1 <<= var2;`把var1的二进制值向左移动var2位，就得到var1的值.

现在我们接触到了更多的运算符，我们将上一章运算符的优先级进行更新：

	++,--(用作前缀);(),+,-(一元前缀);!,~ > *,/,% > +,- > <<,>> > <,>,<=,>= > ==,!= > & 
    > ^ > | > && > || > =,*=,/=,%=,+=,-=,<<=,>>=,&=,^=,|= > ++,--(用作后缀)

#####goto 语句

C#允许给代码直接加上标签，这样就可以使用goto语句直接跳转到这些代码上。该语句优缺点很明显，优点是控制代码何时执行的一种简单方式；缺点是过多使用会使程序变得晦涩难懂。

goto语句用法：

	goto <lableName>;

标签定义：`<lableName>:`

例如下列的代码

	int myInteger =5;
	goto myLable;
	myInteger += 10;
	myLable:
	Console.WriteLine("myInteger = {0}",myInteger);

执行过程就是

- myInteger声明为int型，并赋值5；
- goto中断正常执行顺序，跳转到标有myLable：的代码行上；
- myInteger的值写入控制台。
- 此程序编译时会报错

goto语句有它的作用，但也容易造成混乱，尽量避免使用它。

#####分支

C#中我们常用三种分支技术：三元运算符、if语句、switch语句。

######三元运算符

三元运算符有三个操作数，语法如下：

	<test> ? <resultTrue> : <resultFalse>

eg:`string resultString = (myInteger < 10) ? "Less than 10" : "Greater than or equal to 10";`三元运算符适合简单比较运算赋值的情况，如果比较结果很多，就显然不适合了。

######if语句

if语句是为了有条件的执行其他语句。if语句最简单的语法：

	if (<test>)
		<code executed if <test> is true>;

先执行<test>，如果<test>计算结果是true，就执行下面的代码。执行完毕后，再继续执行后面的代码。如果返回值是false，就忽略if后语句直接执行后面的代码。

	if (<test>)
	<code executed if <test> is true>;
	else
	<code executed if <test> is false>;

这里可以使用花括号将代码放在多个代码行上

	if (<test>)
	{
		<code executed if <test> is true>;
	}
	else
	{
		<code executed if <test> is false>;	
	}

那么上面三元运算符引用的例子就可以像下面这样写：

	string resuitString;
	if (myInteger < 10)
		resultString = "Less than 10";
	else
		resultString = "Greater than or equal 10";

这样写，代码虽然看起来比三元运算符冗长，但便于阅读和理解，也更加灵活。

使用if语句判断多个条件

按照上面的写法我们判断多个条件时必须一层层的嵌套if...else语句，这样非常麻烦，我们这里一般使用else if语句结构。

	if (var1 == 1)
	{
		//do something...
	}
	else if (var1 == 2)
	{
		//do something else...
	}
	else if (var1 == 3 || var1 ==4)
	{
		//do something else...
	}
	else
	{
		//do something else...
	}

这样的结构看起来更加便于阅读，像这样的进行多个条件比较的操作，一般我们会考虑switch语句。

######switch语句

switch可以将测试变量与多个离散的值进行比较。switch语句的基本结构如下：

	switch (<testVar>)
	{
		case <comparisonVal1>:
			<code to execute if <testVar> == <comparisonVal1> >
			break;
		case <comparisonVal2>:
			<code to execute if <testVar> == <comparisonVal2> >
			break;
		...
		case <comparisonValN>:
			<code to execute if <testVar> == <comparisonValN> >
			break;
		default:
			<code to execute if <testVar> != comparisonVals>
			break;
	}


两点注释：

- 测试条件必须和每个case进行比较，如果有一个匹配，就执行匹配值下的语句，如果都不能匹配，就执行default下语句。
- case中的语句执行完，还有一个语句break。这是为了防止执行完一个case后，再去执行另一个case，那样的情况在C#中是非法的。

一个case处理完，不能自由进入下一个case，但是此规则有一个特例。如果把多个case放在一起，其后加代码块，实际上就是一次检查多个条件。如果满足条件中任何一个，就会执行代码，例如：

	switch (<testVar>)
	{
		case <comparisonVar1>:
		case <comparisonVar2>:
			<code to execute if <testVar> == <comparisonVar1> or <testVar> == <comparisonVar2> >
		    break;
		...
	}

case的条件一般都是一个常数值，有时为其直接提供字面值，一种是使用常量。常量与变量类似，但它包含的值是不变的。声明常量需要指定变量类型和关键字const，同时必须赋值。如：

		const int intTwo = 2;

这里注意常量的声明和赋值不能分开写成两行。

#####循环

循环就是重复执行语句。这里主要介绍下面三类循环语句：do循环、while循环、for循环。

######do循环

do循环执行方式：执行循环体内代码，然后进行布尔测试，测试结果为true，就再次执行循环体。直到测试结果为false时，退出循环。do循环结构如下：

	do
	{
		<code to be looped>
	} while (<Test>);

每次循环一次，while都要执行一次Test，返回布尔值来判断是否需要继续循环。

我们写个简单的例子，将1~10数字输出到一列上：

	int i = 1；
	do
	{
		Console.WriteLine("{0}", i++);
	} while (i <= 10);

我们看到，do循环至少都需要执行一次，有些情况下并不是很理想。我们采取while循环来解决这个问题。

######while循环

while循环和do循环非常类似，但有明显区别：while的布尔测试在循环开始时进行，而不是最后。且测试执行结果返回false，就不会执行循环。

while循环体的结构一般是这样的：

	while (<Test>)
	{
		<code to be looped>
	}

使用时也基本上与do循环完全相同，例如打印1到10的数字：

	int i = 1；
	while (i <= 10)
	{
		Console.WriteLine("{0}", i++);
	}

#####for循环

for循环可以执行指定的次数，并维护它自己的计数器。要定义一个for循环，需要下面这些信息：

- 初始化计数器变量的一个起始值；
- 继续循环的条件，它涉及到计数器的变量；
- 在每次循环的最后，对计数器变量执行一个操作。

例如：要在循环中，使计数器从1递增到10，递增量为1，则起始值为1，条件是计数器小于等于10，在每次循环到最后，要执行的操作是给计数器加1。

for循环结构中，如下所示：

	for (<initialization>;<condition>;<operation>)
	{
		<code to loop>
	}

它的工作方式与下述while循环完全相同：

	<initialization>
	while (<condition>)
	{
		<code to loop>
		<operation>
	}

我们用for循环同样输出一下1~10的数字。

	int i;
	for (i = 1; i <= 10; ++i)
	{
		Console.WriteLine("{0}", i);
	}		

#####循环中断

有时候我们需要对代码进行精确的控制处理，C#为此提供了四个命令：

- break——立即终止循环；
- continue——立即终止当前循环，继续执行下一次循环；
- goto——可以跳出循环，到已标记好的位置上，一般不要使用；
- return——跳出循环及其包含的函数。

还是在输出1~10的例子中，使用break：

	int i = 1;
	while (i <= 10)
	{
		if (i == 6)
			break;
		Console.WriteLine("{0}", i++);
	}

这段代码会输出1~5的数字，在i=6时推出循环。

continue仅中断当前循环，而不是整个循环，例如：

	int i;
	for (i = 1; i <= 10; i++)
	{
		if ((i % 2) == 0)
			continue;
		Console.WriteLine(i);
	}

这个例子只要是i除以2余数是0，continue就终止当前循环，所以只显示数字1，3，5，7，9.

使用goto语句如：

	int i = 1;
	while (i <= 10)
	{
		if (i == 6)
			goto exitPoint;
		Console.WriteLine("{0}", i++);
	}
	Console.WriteLine("This code will never be reached.");
	exitPoint:
	Console.WriteLine("This code is run when the loop is exited using goto.");

####Q&A：

Q1、如果两个整数存在变量var1和var2中，该进行什么样的布尔测试，看看其中的一个（不是两个）是否大于10？

Answer：`(var1 > 10) ^ (var2 > 10)`

Q2、编写一个程序，其中包含练习1的逻辑，要求用户输入两个数字，并显示他们，但拒绝接受两个数字都大于10的情况，并要求用户重新输入。

Answer：练习程序如下所示：

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
		            //问题二解决方案
		
		            bool numsOK = false;
		            double var1, var2;
		            var1 = 0;
		            var2 = 0;
		
		            while (!numsOK)
		            {
		                Console.WriteLine("请输入一个数字：");
		                var1 = Convert.ToDouble(Console.ReadLine());
		                Console.WriteLine("请再输入一个数字：");
		                var2 = Convert.ToDouble(Console.ReadLine());
		
		                if ((var1 > 10) && (var2 > 10))
		                {
		                    Console.WriteLine("输入的两个数字不能都大于十，请重新输入！");
		                }
		                else
		                {
		                    numsOK = true;
		                }
		            }
		
		            Console.WriteLine("您输入的数字是{0}和{1}.", var1, var2);
		            Console.ReadKey();
		        }
		    }
		}

Q3、下面的代码有什么问题？

	int i;
	for (i = 1; i <= 10; i++)
	{
		if ((i % 2) = 0)
			continue;
		Console.WriteLine(i);
	}

Answer:代码中if的判断语句`(i % 2) = 0`需要修改为`(i % 2) == 0`。单一的等号用于赋值，双等号才用于判断相等。

