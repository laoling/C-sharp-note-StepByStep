本篇文档我们继续讨论C#的类，主要介绍如何定义字段、属性和方法等类成员。

# 定义类成员 #

## 一、成员定义 ##

前面我们已经介绍了类中字段、方法和属性。并且成员都有访问级别，我们这里详细列举下：

* public  成员可以由任何代码访问；
* private  成员只能由类中的代码访问（如果没有使用任何关键字就默认为这个关键字）。
* internal  成员只能由定义它的程序集（项目）内部代码访问；
* protected  成员只能由类或派生类中的代码访问。
* protected internal 它只能由程序集中派生类的代码来访问。
* static  静态成员。

#### 1.1 定义字段

字段用标准的变量声明格式和前面介绍的修饰符来定义，可以进行初始化，例如：

```csharp
class MyClass
{
	public int MyInt;
}
```

字段也可以使用关键字readonly，表示这个字段只能在执行构造函数的过程中赋值，或由初始化赋值语句赋值。例如：

```csharp
class MyClass
{
	public readonly int MyInt = 17;
}
```

字段也可声明为静态的，例如：

```csharp
class MyClass
{
	public static int MyInt;
}
```

静态字段必须由定义它的类访问，像上面的MyClass.MyInt。而不是通过这个类的对象实例来访问。这里也可以使用const关键字创建常量。按照定义，const成员也是静态的，但不需要static关键字修饰。

#### 1.2 定义方法

方法使用标准函数格式、可访问性和可选的static来声明。例如：

```csharp
class MyClass
{
	public string GetString()
	{
		return "Here is a string.";
	}
}
```

如果使用了static关键字，这个方法就只能通过类来访问，不能通过对象的实例来访问。也可以在方法定义中使用下面的关键字：

* virtual  方法可以重写；
* abstract  方法必须在非抽象的派生类中重写（只用于抽象类中）；
* override  方法重写了一个基类方法（如果方法被重写，就必须使用该关键字）。
* extern  方法定义放在其他地方。

下面的代码是方法重写的一个例子：

```csharp
public class MyBaseClass
{
	public virtual void DoSomething()
	{
		// Base implementation.
	}
}

public class MyDerivedClass : MyBase
{
	public override void DoSomething()
	{
		// Derived class implementation, overrides base implementation.
	}
}
```

如果使用了override，也可以使用sealed指定在派生类中不能对这个方法作进一步修改，即这个方法不能由派生类重写。例如：

```csharp
public class MyDerivedClass : MyBase
{
	public override sealed void DoSomething()
	{
		// Derived class implementation, overrides base implementation.
	}
}
```

使用extern可以在项目外部提供方法的实现代码，这里不做讨论。

#### 1.3 定义属性

属性定义方法与字段类似，但包含的内容比较多。属性拥有两个类似于函数的块，一块用于获取属性的值，一块用于设置属性的值。这两个块也称为访问器，分别用get和set关键字来定义，可以用于控制对属性的访问级别。可以忽略其中的一个块来创建只读或只写属性（具体忽略get块创建只写属性，忽略set块创建只读属性）。

属性的基本结构包括标准的可访问修饰符，后跟类名、属性名和get块（set块  get和set块 其中包含属性处理代码），例如：

```csharp
public int MyIntProp
{
	get
	{
		// Property get code.
	}
	set
	{
		// Property set code.
	}
}
```

定义代码非常类似于定义字段，区别是行末没有分号，而是一个包含嵌套get和set的代码块。get块必须有一个属性类型的返回值，简单的属性一般与私有字段相关联，以控制对这个字段的访问，此时get块可以直接访问该字段的值，例如：

```csharp
// Field used by property.
private int myInt;

//Property.
public int MyIntProp
{
	get
	{
		return myInt;
	}
	set
	{
		//Property set code.
	}
}
```

类外部代码不能直接访问这个myInt字段，因为其访问级别是私有的。外部代码必须使用属性来访问该字段。set函数以类似的方式把一个值赋给字段。这里可以使用关键字value表示用户提供的属性值：

```csharp
// Field used by property.
private int myInt;

//Property.
public int MyIntProp
{
	get
	{
		return myInt;
	}
	set
	{
		myInt = value;
	}
}
```

value等于类型与属性相同的一个值，所以如果属性和字段使用相同的类型，就不必担心数据类型转换了。在对操作进行更多控制时，属性的真正作用才能发挥出来。

eg： 使用下面的set块。

```csharp
set
{
	if (value >= 0 && value <= 10)
		myInt = value;
}
```

这里只有赋给属性值在0~10之间才会改myInt。此时如果使用了无效值，该怎么办？有四种选择：

`什么也不做`   `给字段赋默认值`   `继续执行，就好像未发生错误，单记录下该事件，以备分析`  `抛出异常`

一般来说，后两种选择效果比较好。抛出异常给用户提供控制权很大，可以让他们知道发生了什么情况，并作出适当的反应，为此可以使用System命名空间中的标准异常，如下：

```csharp
set
{
	if (value >= 0 && value <= 10)
		myInt = value;
	else
		throw (new ArgumentOutOfRangeException("MyIntProp", value, "MyIntProp must be assigned a value between 0 and 10."));
}
```

属性可以使用virtual、override、abstract关键字，就像方法一样，但这几个关键字不能用于字段。访问器可以有自己的可访问性。例如：

```csharp
// Field used by property.
private int myInt;

//Property.
public int MyIntProp
{
	get
	{
		return myInt;
	}
	protected set
	{
		myInt = value;
	}
}
```

访问器可以使用的访问修饰符取决于属性的可访问性，访问器的可访问性不能高于它所属的属性。也就是说，私有属性对它的访问器不能包含任何可访问修饰符，而公有属性可以对其访问器使用所有的可访问修饰符。


#### 1.4 自动属性

利用自动属性可以简化语法声明属性。C#编译器会自动添加未键入的内容。具体而言编译器会声明一个用于存储属性的私有字段，并在属性的get和set块中使用该字段，我们无需考虑细节。使用下面的代码既可以定义一个自动属性：

```csharp
public int MyIntProp
{
	get;
	set;
}
```

甚至可以在一行代码中定义自动属性，以便节省空间，而不会过度降低属性的可读性：

```csharp
public int MyIntProp {get; set;}
```

自动属性唯一的限制是它们必须包含get和set存取器，无法使用这种方式定义只读或只写属性。


## 二、类成员的其他话题 ##

#### 2.1 隐藏基类方法

当从基类继承一个非抽象的成员时，也就继承了其实现代码。如果继承的成员是虚拟的，就可以用override关键字重写这段实现代码。无论继承的成员是否为虚拟，都可以隐藏这些实现代码。实际中是很有用的，比如当继承的公共成员不像预期的那样工作时，就可以隐藏它。代码如下：

```csharp
public class MyBaseClass
{
	public void DoSomething()
	{
		//Base implementation.
	}
}

public class MyDerivedClass : MyBaseClass
{
	public void DoSomething()
	{
		// Derived class implementation, hides base implementation.
	}
}
```

这段代码运行正常，但会产生一个警告，说明隐藏了一个基类成员。如果无意间隐藏的，就要改正错误。如果确实要隐藏该成员，就可以使用new关键字显式地表达意图：

```csharp
public class MyDerivedClass : MyBaseClass
{
	new public void DoSomething()
	{
		// Derived class implementation, hides base implementation.
	}
}
```

注意隐藏基类成员和重写它们的区别。看下面的代码：

```csharp
public class MyBaseClass
{
	public virtual void DoSomething()
	{
		Console.WriteLine("Base imp");
	}
}

public class MyDerivedClass : MyBaseClass
{
	public override void DoSomething()
	{
		Console.WriteLine("Derived imp");
	}
}
```

其中重写方法将替换基类中的实现代码，这样，下面的代码就将使用新版本，即使这是通过基类类型进行的，情况也是这样（使用多态性）：

```csharp
MyDerviedClass myObj = new myDerivedClass();
MyBaseClass myBaseObj;
myBaseObj = myObj;
myBaseObj.DoSomething();
```

结果如下：`Derived imp`

另外还可以使用下面的代码隐藏基类方法：

```csharp
public class MyBaseClass
{
	public virtual void DoSomething()
	{
		Console.WriteLine("Base imp");
	}
}

public class MyDerivedClass : MyBaseClass
{
	new public void DoSomething()
	{
		Console.WriteLine("Derived imp");
	}
}
```

基类的方法不必是虚拟的，但结果是一样的，只需要改动一行即可。对于基类的虚拟方法和非虚拟方法来说，其结果如下：`Base imp`

尽管隐藏了基类的实现代码，但仍可以通过基类访问它。

#### 2.2 调用重写或隐藏的基类方法

无论是重写成员还是隐藏成员，都可以在派生类内部访问基类成员。这在许多情况下都是很有用的：

* 要对派生类的用户隐藏继承的公共成员，但仍能在类中访问其功能。
* 要给继承的虚拟成员添加实现代码，而不是简单地用重写的新执行代码替换它。

可以使用base关键字，它表示包含在派生类中的基类的实现代码。例如：

```csharp
public class MyBaseClass
{
	public virtual void DoSomething()
	{
		//Base implementation.
	}
}

public class MyDerivedClass : MyBaseClass
{
	public override void DoSomething()
	{
		//Derived class implementation, extends base class implementation.
		base.DoSomething();
		//More dervied class implementation.
	}
}
```

这段代码执行包含在MyBaseClass中的DoSomething()版本，MyBaseClass是MyDerivedClass的基类，而DoSomething()版本包含在MyDerivedClass中。因为base使用的是对象实例，所以在静态成员中使用它会出错。

【this关键字】

除了base关键字，这里还能使用this关键字。与base一样，this也可以用在类成员的内部，且该关键字也引用对象实例。只是this引用的是当前的对象实例（即不能在静态成员中使用this关键字，因为静态成员不是对象实例的一部分）。

this关键字最常用的功能是把当前对象实例的引用传递给一个方法。如下例：

```csharp
public void doSomething()
{
	MyTargetClass myObj = new MyTargetClass();
	myObj.DoSomethingWith(this);
}
```

其中被实例化的MyTargetClass实例有一个DoSomethingWith()方法，该方法带一个参数，其类型与包含上述方法的类兼容。这个参数类型可以是类的类型、由这个类继承的类类型，或者由这个类或System.Object实现的一个接口。

this关键字的另一个常见用法是限定本地类型的成员，例如：

```csharp
public class MyClass
{
	private int someData;

	public int someData
	{
		get
		{
			return this.someData;
		}
	}
}
```

#### 2.3 嵌套的类型定义

除了在命名空间中定义类外，还可以在其他类中定义类。如果这么做，就可以在定义中使用各种访问修饰符，而不仅仅是public和internal，也可以使用new关键字隐藏继承于基类的类型定义。

eg： 在定义了MyClass中也定义了一个嵌套的类myNestedClass:

```csharp
public class MyClass
{
	public class myNestedClass
	{
		public int nestedClassField;
	}
}
```

如果要在MyClass外部实例化myNestedClass，就必须限定名称，如：

```csharp
MyClass.myNestedClass myObj = new MyClass.myNestedClass();
```

这个功能主要用于定义对于其包含类来说是私有的类，这样，命名空间中的其他代码就不能访问它。


## 三、接口的实现 ##

这里介绍下如何定义和实现接口。前面我们介绍过接口的定义：

```csharp
interface IMyInterface
{
	//Interface members.
}
```

接口成员的定义也与类成员定义相似。但有几个重要区别：

* 不允许使用访问修饰符（public、private、protected、internal），所有的接口成员都是公共的。
* 接口成员不能包含代码体。
* 接口不能定义字段成员。
* 接口成员不能使用关键字static、virtual、abstract、sealed来定义。
* 类型定义成员是禁止的。

但要隐藏继承了基接口的成员，可以用关键字new来定义它们，例如：

```csharp
interface IMyBaseInterface
{
	void DoSomething();
}

interface IMyDerivedInterface : IMyBaseInterface
{
	new void DoSomething();
}
```

其执行方式和隐藏继承的类成员的方式一样。在接口中定义的属性可以定义访问块get和set中的哪一个能用于该属性（或同时用于该属性）。例如：

```csharp
interface IMyInterface
{
	int MyInt { get; set;}
}
```

其中int属性MyInt有get和set存取器。对于访问级别有更严格限制的属性来说，可以省略它们中的任何一个。这个语法类似于自动属性，但自动属性是为类定义的，自动属性必须包含get和set存取器。

接口没有指定应如何存储属性数据。接口不能指定字段，例如用于存储属性数据的字段。最后接口和类一样，可以定义为类的成员，但不能定义为其他接口的成员，因为接口不能包含类型定义。

【在类中实现接口】

实现接口的类必须包含该接口所以成员的实现代码，且必须匹配指定的签名，包括匹配指定的get和set块，并且必须是公共的。例如：

```csharp
public interface IMyInterface
{
	void DoSomething();
	void DoSomethingElse();
}

public class MyClass : IMyInterface
{
	public void DoSomething()
	{
	}

	public void DoSomethingElse()
	{
	}
}
```

可以使用关键字virtual或abstract来实现接口成员，但不能使用static或const。还可以在基类上实现接口成员，例如：

```csharp
public interface IMyInterface
{
	void DoSomething();
	void DoSomethingElse();
}

public class MyBaseClass
{
	public void DoSomething()
	{
	}
}

public class MyDerivedClass : MyBaseClass, IMyInterface
{
	public void DoSomethingElse()
	{
	}

}
```

继承一个实现给定接口的基类，就意味着派生类隐式地支持这个接口，例如：

```csharp
public interface IMyInterface
{
	void DoSomething();
	void DoSomethingElse();
}

public class MyBaseClass : IMyInterface
{
	public virtual void DoSomething()
	{
	}

	public virtual void DoSomethingElse()
	{
	}
}

public class MyDerivedClass : MyBaseClass
{
	public override void DoSomething()
	{
	}

}
```

显然在基类中把实现代码定义为虚拟，派生类就可以替换该实现代码，而不是隐藏它们。如果要使用new关键字隐藏一个基类成员，而不是重写它，则方法IMyInterface.DoSomething()就总是引用基类版本，即使通过这个接口来访问派生类，也是这样。

1、显式实现接口成员

可以由类显式的实现接口成员。这么做，该成员只能通过接口来访问，不能通过类来访问。

如果隐式实现成员，可以通过类和接口来访问。如：类MyClass隐式实现接口IMyInterface的方法DoSomething()，下面的代码就是有效的：

```csharp
MyClass myObj = new MyClass();
myObj.DoSomething();
```

或这样也是有效的：

```csharp
MyClass myObj = new MyClass();
IMyInterface myInt = myObj;
myInt.DoSomething();
```

另外如果MyDerivedClass显式实现DoSomething()，就只能使用后一种技术。代码如下：

```csharp
public class MyClass : IMyInterface
{
	void IMyInterface.DoSomething()
	{
	}

	public void DoSomethingElse()
	{
	}
}
```

其中DoSomething()是显式实现的，而DoSomethingElse()是隐式实现的。后者才能通过MyClass对象实例来访问。

2、用非公共的可访问性添加属性存取器

前面说过如果实现带属性的接口，就必须实现匹配的get/set存取器。这并不是绝对正确的，如果在定义属性的接口中只包含set块，就可以给类中的属性添加get块，反之亦然。但是只有所添加的存取器的可访问修饰符比接口中定义的存取器的可访问修饰符更严格时，才能这么做。因为按照定义，接口定义的存取器是公共的，也就是说，只能添加非公共的存取器。例如：

```csharp
public interface IMyInterface
{
	int MyIntProperty
	{
		get;
	}
}

public class MyBaseClass : IMyInterface
{
	public int MyIntProperty { get; protected set; }
}
```

## 四、部分类定义 ##

创建的类有许多成员时，代码文件比较长。这里可以采用前面介绍的给代码分组。在代码中定义区域，就可以折叠展开各个代码区，使代码易于阅读。

```csharp
public class MyClass
{
	#region Field
	private int myInt;
	#endregion

	#region Constructor
	public MyClass()
	{
		myInt = 99;
	}
	#endregion

	#region Properties
	public int MyInt
	{
		get
		{
			return myInt;
		}
		set
		{
			myInt = value;
		}
	}
	#endregion

	#region Methods
	public void DoSomething()
	{
		//Do Something...
	}
	#endregion
}
```

这段代码可以展开折叠类的字段、属性、构造函数和方法，以便集中精神考虑自己感兴趣的内容。但有时候，使用这样的技术代码依然难以理解。对此一个方法是使用部分类定义，partial class definition。简言之，就是使用部分类定义，把类的定义放在多个文件中。

eg： 可以把字段、属性和构造函数放在一个文件中，而把方法放在另一个文件中。为此，只需要在每个包含部分类定义的文件中对类使用partial关键字即可，如下：

```csharp
public partial class MyClass
{
	...
}
```

如果使用部分类定义，partial关键字就必须出现在包含定义部分的每个文件的与此相同的位置。

应用于部分类的接口也会应用于整个类，下面的两个定义等价：

```csharp
public partial class MyClass : IMyInterface1
{
	...
}

public partial class MyClass : IMyInterface2
{
	...
}
```
和
```csharp
public class MyClass : IMyInterface1, IMyInterface2
{
	...
}
```

部分类定义可以在一个部分类定义文件或者多个部分类定义文件中包含基类。但如果基类在多个定义文件中指定，它就必须是同一个基类，因为在C#中，类只能继承一个基类。


## 五、部分方法定义 ##

部分类也可以定义部分方法。部分方法在部分类中定义，但没有方法体，另一个部分类中包含实现代码。在这个两个部分类中，都要用partial关键字。

```csharp
public partial class MyClass
{
	partial void MyPartialMethod();
}

public partial class MyClass
{
	partial void MyPartialMethod()
	{
		//Method implementation
	}
}
```

部分方法也可以是静态的，但他们总是私有的，且不能有返回值。它们在使用的任何参数都不能是out参数，但可以是ref参数。部分方法也不能使用virtual、abstract、override、new、sealed、extern修饰符。

与部分类一样，部分方法在定制自动生成代码或设计器创建代码时，都是很有用的。设计器会声明部分方法，用户根据具体情形选择是否实现它。如果不实现它，就不会影响性能，因为该方法在编译过的代码中不存在。


## Q&A ##

Q1、编写代码，定义一个基类MyClass，其中包含虚拟方法GetString()。这个方法应返回存储在受保护字段myString中的字符串，该字段可以通过只写公共属性ContainedString来访问。

Answer:参见下面的代码

```csharp
class MyClass
{
	public string ContainedString
	{
		set
		{
			myString = value;
		}
	}

	public virtual string GetString()
	{
		return myString;
	}
}
```

Q2、从类MyClass中派生一个类MyDerivedClass，重写GetString()方法，使用该方法的基类实现代码从基类中返回一个字符串，但在返回的字符串中添加文本“（output from derived class）”。

Answer:参见下面的代码

```csharp
class MyDerivedClass : MyClass
{
	public override string GetString()
	{
		return base.GetString() + "(output from derived class)";
	}
}
```

Q3、部分方法定义必须使用void返回类型。说明其原因。

Answer:部分方法定义与实现基本上都是分开的，给定义提供部分方法的实现代码时，编译器就需要执行这个方法，否则就忽略这个方法，在编译后代码中删除这个方法。

要达到这个效果，使用void是最安全的办法，没有返回类型的方法不能作为表达式的一部分来调用，编译器就可以安全删除对部分方法调用的所有引用。

Q4、编写一个类MyCopyableClass，该类可以使用方法GetCopy()返回它本身的一个副本。这个方法应使用派生于System.Object的MemberwiseClone()方法。给该类添加一个简单的属性，并且编写客户代码，客户代码使用该类检查任务是否成功执行。

Answer:参见下面的代码

```csharp
class MyCopyableClass
{
	protected int myInt;
	
	public int ContainedInt
	{
		get
		{
			return myInt;
		}
		set
		{
			myInt = value;
		}
	}

	public MyCopyableClass GetCopy()
	{
		return (MyCopyableClass)MemberwiseClone();
	}
}

...
class Program
{
	static void Main(string[] args)
	{
		MyCopyableClass obj1 = new MyCopyableClass();
		obj1.ContainedInt = 5;
		MyCopyableClass obj2 = obj1.GetCopy();
		obj1.ContainedInt = 9;
		Console.WriteLine(obj2.ContainedInt);
		Console.ReadKey();
	}
}
```
客户代码运行显示5.

Q5、为Chapter10CardLib库编写一个控制台客户程序，从搅乱的Deck对象中一次取出5张牌。如果这5张牌都是相同的花色，客户程序就应在屏幕上显示这五张牌，以及文本“Flush！”，否则就显示52张牌以及文本“No flush”，并退出。

Answer：参见下面的代码

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chapter10CardLib;

namespace ExerciseAnswer
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Deck playDeck = new Deck();
                playDeck.Shuffle();
                bool isFlush = false;
                int flushHandIndex = 0;
                for (int hand = 0; hand < 10; hand++)
                {
                    isFlush = true;
                    Suit flushSuit = playDeck.GetCard(hand * 5).suit;
                    for (int card = 0; card < 5; card++)
                    {
                        if (playDeck.GetCard(hand * 5 + card).suit != flushSuit)
                        {
                            isFlush = false;
                        }
                    }
                    if (isFlush)
                    {
                        flushHandIndex = hand * 5;
                        break;
                    }

                }

                if (isFlush)
                {
                    Console.WriteLine("Flush!");
                    for (int card = 0; card < 5; card++)
                    {
                        Console.WriteLine(playDeck.GetCard(flushHandIndex + card));
                    }
                }
                else
                {
                    Console.WriteLine("No flush.");
                }
                Console.ReadLine();
            }
        }
    }
}

```