此文档主要记录下C#入门经典第三章：变量与表达式这一章的内容笔记。

#####C#的基本语法

1. 代码段一般包裹在定界符花括号之内{}。
2. 注释风格1：注释开头结尾标记，可以使单行注释，也可以是多行注释。/**/
3. 注释风格2：使用//开始一个注释。只用于单行注释。
4. 注释风格2的扩展：使用三个/，这类注释可以通过IDE的配置，被自动提取生成文档说明书。
5. C#代码区分大小写。

        /*这是第一种注释风格 */
        /*也可以进行多行注释，第一行
          第二行
          ......
          第N行*/
        //这是第二种注释风格
        ///这是特殊的注释风格

在C#中，遇到以#开头的代码一般是预处理指令，这里介绍两个，#region #endregion，这两个关键字可以定义可展开和折叠的代码区域的开头和结尾。


#####变量及用法

声明变量的C#语法是指定类型和变量名：`<type> <name>;`

基本类型：

整数类型

<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
<table class="table table-bordered table-striped table-condensed">
   <tr>
      <td>&#31867;&#22411;</td>
      <td>&#21035;&#21517;</td>
      <td>&#20540;</td>
   </tr>
   <tr>
      <td>sbyte</td>
      <td>System.Sbyte</td>
      <td>&#22312;-128~127&#38388;&#25972;&#25968;</td>
   </tr>
   <tr>
      <td>byte</td>
      <td>System.Byte</td>
      <td>&#22312;0~255&#38388;&#25972;&#25968;</td>
   </tr>
   <tr>
      <td>short</td>
      <td>System.Int16</td>
      <td>&#22312;-32768-32767&#38388;&#25972;&#25968;</td>
   </tr>
   <tr>
      <td>ushort</td>
      <td>System.UInt16</td>
      <td>&#22312;0-65535&#38388;&#25972;&#25968;</td>
   </tr>
   <tr>
      <td>int</td>
      <td>System.Int32</td>
      <td>&#30053;</td>
   </tr>
   <tr>
      <td>uint</td>
      <td>System.UInt32</td>
      <td>&#30053;</td>
   </tr>
   <tr>
      <td>long</td>
      <td>System.Int64</td>
      <td>&#30053;</td>
   </tr>
   <tr>
      <td>ulong</td>
      <td>System.UInt64</td>
      <td>&#30053;</td>
   </tr>
   <tr>
      <td></td>
   </tr>
</table>

浮点型数字类型：

<table class="table table-bordered table-striped table-condensed">
   <tr>
      <td>&#31867;&#22411;</td>
      <td>&#21035;&#21517;</td>
      <td>&#20540;</td>
   </tr>
   <tr>
      <td>float</td>
      <td>System.Single</td>
      <td>&#32422;1.5*10^-45~3.4*10^38</td>
   </tr>
   <tr>
      <td>double</td>
      <td>System.Double</td>
      <td>&#32422;5*10^-324~1.7*10^308</td>
   </tr>
   <tr>
      <td>decimal</td>
      <td>System.Decimal</td>
      <td>&#32422;1*10^-28~7.9*10^28</td>
   </tr>
   <tr>
      <td></td>
   </tr>
</table>

其他数据类型：

<table class="table table-bordered table-striped table-condensed">
   <tr>
      <td>类型</td>
      <td>别名</td>
      <td>值</td>
   </tr>
   <tr>
      <td>char</td>
      <td>System.Char</td>
      <td>一个Unicode字符，0—65535间的整数</td>
   </tr>
   <tr>
      <td>bool</td>
      <td>System.Boolean</td>
      <td>布尔值，true或false</td>
   </tr>
   <tr>
      <td>string</td>
      <td>System.String</td> 
      <td>字符串</td>
   </tr>
</table>

变量的命名：

原则：

- 变量名的第一个字符必须是字母、下划线或@。
- 其后的字符可以使字母，下划线或数字。
- 变量中不要轻易使用系统关键字。
- 变量名在C#中区分大小写。

命名约定：

camelCase：有多个单词组成的变量名中，第一个单词小写字母开头，其余单词大写开头。

		age
		firstName
		timeOfDetch

PascalCase：由多个首字母大写的单词构成。

		Age
		FirstName
		WinterOfDiscontent

过去还有一中变量名命名系统，多个单词间以下划线分隔，如yet_another_variable。已经淘汰了。

微软建议，对于简单变量，使用camelCase命名规则。对于比较高级的命名规则使用PascalCase。

Unicode转义序列在变量赋值字面值中有很多用途。一个变量中多处需要转义，只需要在字符串引号前加一个@。例如："C:\\Temp\\MyDir\\MyFile.doc" 就可以这样写：@"C:\Temp\MyDir\MyFile.doc"

变量的声明和赋值：

- 单一变量： `int age; age = 25;`
- 多个变量声明： `int xSize, ySize;`
- 变量在声明时同时赋值： `int age = 25;`
- (多个)变量在声明时赋值： `int xSize = 4, ySize = 5;`

#####表达式及用法：

- 将变量和值用运算符组合起来，就可以创建表达式。
- 运算符按操作数分可以分为一元运算符、二元运算符、三元运算符三类。
- 运算符按用途可以分为数学运算符、赋值运算符、递增（递减）运算符、逻辑运算符、条件运算符。

这里我们先主要看一下数学运算符和赋值运算符。

- +：`var1 = var2 + var3;`二元，表示var2和var3的和是var1，也可以表示字符串var1是var2和var3相连得       到的；
- +： `var1 = +var2;`一元，var1的值等于var2的值；
- -：`var1 =var2 - var3;`二元，var1的值等于var2减去var3;
- -：`var1 = -var2;`一元，var1的值等于var2的值乘以-1；
- *：`var1 = var2 * var3;`二元，var1的值等于var2乘以var3;
- /：`var1 = var2 / var3;`二元，var1的值等于var2除以var3;
- %：`var1 = var2 % var3;`二元，var1的值等于var2除以var3所得余数;

递增递减运算符：

- ++：一元，`var1 = ++var2;`var1的值等于var2+1,var2递增1；
- ++：一元，`var1 = var2++;`var1的值等于var2，var2递增1；
- --：一元，`var1 = --var2;`var1的值等于var2-1，var2递减1；
- --：一元，`var1 = var2--;`var1的值等于var2,var2递减1；

赋值运算符：

- =：二元，`var1 = var2;`var1被赋予var2的值；
- +=：二元，`var1 += var2;`var1被赋予var1和var2的和；
- -=：二元，`var1 -= var2;`var1被赋予var1和var2的差；
- *=：二元，`var1 *= var2;`var1被赋予var1和var2的积；
- /=：二元，`var1 /= var2;`var1被赋予var1和var2相除的商；
- %=：二元，`var1 %= var2;`var1被赋予var1和var2相除的余数；

运算符优先级：

由高到低由左至右的顺序排列如下：

`++,--;+,-;(用作前缀) > *,/,% > +,- > =,*=,/=,%=,+=,-= > ++,--(用作后缀)`

#####命名空间：

我们这里主要看实例1中的代码

    using System;
    using System.Collections.Generic;
    using.System.Linq;
    using.System.Text;

    namespace ConsoleExampleApplication1
    {
      //code here......
    }

代码开头的四行代码声明了这段代码使用了System、System.Collections.Generic、System.Linq、System.Text命名空间，在这个文件中，我们可以访问这些命名空间，无需分类。System命名空间是.NET Framework程序的根命名空间，包含了控制台应用程序所有基本功能。最后，我们还为本程序声明了一个命名空间ConsoleExampleApplication1。

#####Q&A

Q1、在下面的代码中，如何从命名空间fabulous的代码中引用名称great？

		namespace fabulous
		{
		  //code in fabulous namespace
		}

		namespace super
		{
			namespace smashing
			{
				// great name defined
			}
		}


Answer: `super.smashing.great`

Q2、下面哪些变量名不合法？

	myVariableIsGood
	99Flake
	_floor
	time2GetJiggyWidIt
	wrox.com

Answer: 99Flake 、 wrox.com

Q3、字符串supercalifragilisticexpialidocious是因为太长而不能放在string变量中吗？为什么？

Answer：string可以放入此字符串，不会超出限制长度。

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
		            string s;
		            s = "supercalifragilisticexpialidocious";
		            Console.WriteLine(s);
		            string supercalifragilisticexpialidocious;
		            supercalifragilisticexpialidocious = "this is a long string";
		            Console.WriteLine(supercalifragilisticexpialidocious);
		            Console.ReadKey();
		        }
		    }
		}

在VCE2010中运行，结果显示supercalifragilisticexpialidocious/n this is a long string。无报错无警告。

Q4、考虑运算符的优先级，列出下列表达式的计算步骤。

	resultVar += var1 * var2 + var3 % var4 / var5;

Answer:具体步骤如下所示

	result0 = var1 * var2;
	result1 = var4 / var5;
	result2 = var3 % result1;
	result3 = result0 + result2;
	resultVar = resultVar + result3;

Q5、编写一个控制台应用程序，要求用户输入4个int值，并显示他们的乘积。提示：可以使用Convert.ToDouble()命令把用户在控制台上输入的数转换为double；从string转换为int的命令为Convert.ToInt32()。

Answer：分析下，一个int值最大是2*10^9,四个int值相乘，最大可达到10^32，double双精度浮点数可达到10^308,显示题目中的乘积没有问题。

具体代码如下所示

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
		            int firstNumber, secondNumber, thirdNumber, fourthNumber;
		            double productNumber;
		
		            Console.WriteLine("请输入第一个整数：");
					//输入的数字不是整型时会报错，所以这里Convert.ToInt32()必不可少
		            firstNumber = Convert.ToInt32(Console.ReadLine());
		            Console.WriteLine("请输入第二个整数：");
		            secondNumber = Convert.ToInt32(Console.ReadLine());
		            Console.WriteLine("请输入第三个整数：");
		            thirdNumber = Convert.ToInt32(Console.ReadLine());
		            Console.WriteLine("请输入第四个整数：");
		            fourthNumber = Convert.ToInt32(Console.ReadLine());
		            productNumber = Convert.ToDouble(firstNumber) * Convert.ToDouble(secondNumber) * Convert.ToDouble(thirdNumber) * Convert.ToDouble(fourthNumber);
		
		            Console.WriteLine("整数{0}和{1}和{2}和{3}的乘积是{4}！", firstNumber, secondNumber, thirdNumber, fourthNumber, productNumber);
		            Console.ReadKey();
		
		        }
		    }
		}

