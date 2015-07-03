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



