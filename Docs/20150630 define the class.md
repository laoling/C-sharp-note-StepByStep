本篇笔记我们来介绍定义类的相关内容。

# 定义类 #

## 一、C#中类的定义 ##

C#中使用class关键字来定义类：

```csharp
class MyClass
{
	// Class members.
}
```

这里定义了一个类MyClass，定义类后，就可以在项目中能访问该定义的其他位置对该类实例化。默认的，类声明是内部的，即只有当前项目中的代码才能访问它。可以使用internal访问修饰符关键字显式指定，如下所示（但不必要）：

```csharp
internal class MyClass
{
	// Class members.
}
```

另外可以指定类是公共的，应该可以由其他项目中的代码来访问。为此需要使用关键字public。

```csharp
public class MyClass
{
	// Class members.
}
```

除了上面的，还可以指定类是抽象的（不能实例化，只能继承，可以有抽象成员）或者是密封的（不能继承），可以使用两个互斥的关键字abstract 或 sealed。所有抽象类声明：

```csharp
public abstract class MyClass
{
	// Class members, may be abstract.
}
```

密封类的声明如下：

```csharp
public sealed class MyClass
{
	// Class members.
}
```
还可以在类定义中指定继承。为此，要在类名的后面加上一个冒号，其后是基类名，例如：

```csharp
public class MyClass : MyBase
{
	// Class members.
}
```

在C#的类定义中，只能有一个基类，如果继承了一个抽象类，就必须实现所继承的所有抽象成员。（除非派生类也是抽象的）。

编译器不允许派生类的可访问性高于基类。也就是说，内部类可以继承于一个公共基类，但公共类不能继承于一个内部类。

除了以这种方式指定基类外，还可以再冒号后面指定支持的接口。如果指定了基类，必须紧跟在冒号后面，之后才是指定的接口。必须使用逗号分隔基类名和接口名。

eg： 给MyClass添加一个接口：

```csharp
public class MyClass : IMyInterface
{
	// Class members.
}
```

指定基类和接口：

```csharp
public class MyClass : MyBase, IMyInterface
{
	// Class members.
}
```

指定多个接口：

```csharp
public class MyClass : MyBase, IMyInterface, IMySecondInterface
{
	// Class members.
}
```

【修饰符】

1. 无或internal： 只能在当前项目中访问类；
2. public： 可以在任何地方访问类；
3. abstract 或 internal abstract： 类只能在当前项目中访问，不能实例化，只能供继承之用；
4. public abstract： 类可以在任何地方访问，不能实例化，只能供继承之用；
5. sealed 或 internal sealed： 类只能在当前项目中访问，不能供派生之用，只能实例化；
6. public sealed： 类可以在任何地方访问，不能供派生之用，只能实例化。

【接口的定义】

声明接口和类相似，但使用的关键字是interface。

```csharp
interface IMyInterface
{
	// Interface members.
}
```

接口和类一样，默认是内部接口。所以接口公开访问，必须使用public关键字：

```csharp
public interface IMyInterface
{
	// Interface members.
}
```

接口的继承也可以用与类继承相似的方式指定。主要区别是可以使用多个基接口，例如：

```csharp
public interface IMyInterface : IMyBaseInterface, IMyBaseInterface2
{
	// Interface members.
}
```

原来说过，不能用实例化类的方式实例化接口。

## 二、System.Object ##

System.Object包含的方法如下：

1. Object()  虚拟：无  静态：无  System.Object类型的构造函数，由派生类型的构造函数自动调用；
2. ~Object() 虚拟：无  静态：无  也称为Finalize()，System.Object类型的析构函数，由派生类型的析构函数自动调用，不能手动调用；
3. Equals(object)  返回类型：bool  虚拟：有  静态：无  把调用该方法的对象与另一个对象相比，如果它们相等，就返回true。默认的实现代码会查看对象的参数是否引用了同一个对象（因为对象是引用类型）。如果想以不同的方式来比较对象，则可以重写该方法，例如比较两个对象的状态；
4. Equals(object,object)  返回类型：bool  虚拟：无  静态：有  这个方法比较传送给它的两个对象，看看它们是否相等。检查时使用了Equals(object)方法。注意如果两个对象都是空引用，这个方法就返回true；
5. ReferenceEquals(object,object)  返回类型：bool  虚拟：无  静态：有  这个方法比较传送给它的两个对象，看看它们是否是同一个实例的引用；
6. ToString()  返回类型：String  虚拟：有  静态：无  返回一个对应于对象实例的字符串。默认情况下，这是一个类类型的限定名称。但可以重写它，给类型提供合适的实现方式；
7. MemberwiseClone()  返回类型：object  虚拟：无  静态：无  通过创建一个新对象实例并复制成员，以复制该对象。成员拷贝不会得到这些成员的新实例。新对象的任何引用类型成员都将引用与源类相同的对象，这个方法是受保护的。所以只能在类或派生的类中使用；
8. GetType()  返回类型：System.Type  虚拟：无  静态：无  以System.Type对象的形式返回对象的类型；
9. GetHashCode()  返回类型：int  虚拟：有  静态：无  用作对象的散列函数，这是一个必选函数，返回一个以压缩形式标识的对象状态的值。

这些是.NET Framework中对象类型必须支持的基本方法。但我们可能从不使用其中某些类型。


## 三、构造函数和析构函数 ##

建立代码时，编译器一般会自动添加构造函数和析构函数。但是如果需要，可以提供自己的构造函数和析构函数，以便初始化对象和清理对象。

下面语法可以把简单的构造函数添加到列表中：

```csharp
class MyClass
{
	public MyClass()
	{
		//Constructor code.
	}
}
```

此构造函数与包含它的类同名，且没有参数（使之成为类的默认构造函数），这是一个公共函数，所以类的对象可以使用这个函数进行实例化。也可使用私有的默认构造函数，即不能使用这个构造函数来创建这个类的对象实例。

```csharp
class MyClass
{
	private MyClass()
	{
		// Constructor code.
	}
}
```

最后也可以用相同的方法给类添加非默认的构造函数，其方法是提供参数，如：

```csharp
class MyClass
{
	pubilc MyClass()
	{
		// Default constructor code.
	}

	public MyClass()
	{
		// Mondefault constructor code (uses myInt).
	}
}
```

可提供的构造函数数量不受限制。

我们使用略微不同的语法来声明析构函数。在.NET中使用析构函数叫做Finalize()，但这不是我们用于声明析构函数的名称。使用下面的代码：

```csharp
class MyClass
{
	~MyClass()
	{
		// Destructor body.
	}
}
```

类的析构函数是由~前缀的类名来声明。在调用这个析构函数后，还隐式调用基类的析构函数，包括System.Object根类中的Finalize()调用。这个技术可以让.NET Framework确保调用Finalize()。

【构造函数的执行序列】

为了实例化派生的类必须实例化它的基类。进一步必须实例化这个基类的基类，一直到实例化System.Object为止。结果无论使用什么构造函数实例化一个类，总是首先调用System.Object.Object。无论在派生类上使用什么构造函数，除非明确指定，否则就使用基类的默认构造函数。下面我们写个例子，简单说明执行的顺序：

```csharp
public class MyBaseClass
{
	public MyBaseClass()
	{
	}

	public MyBaseClass(int i)
	{
	}
}

public class MyDerivedClass : MyBaseClass
{
	public MyDerivedClass()
	{
	}

	public MyDerivedClass(int i)
	{
	}

	public MyDerivedClass(int i, int j)
	{
	}
}
```

如果以下的方式实例化MyDerivedClass：

```csharp
MyDerivedClass myObj = new MyDerivedClass();
```

则执行的顺序如下：

执行System.Object.Object()构造函数 > 执行MyBaseClass.MyBaseClass()构造函数 > 执行MyDerivedClass.MyDerivedClass()构造函数。

如果使用下面的语句：

```csharp
MyDerivedClass myObj = new MyDerivedClass(4);
```

则执行的顺序如下：

执行System.Object.Object()构造函数 > 执行MyBaseClass.MyBaseClass()构造函数 > 执行MyDerivedClass.MyDerivedClass(int i)构造函数。

如果使用下面的语句：

```csharp
MyDerivedClass myObj = new MyDerivedClass(4, 8);
```

则执行的顺序如下：

执行System.Object.Object()构造函数 > 执行MyBaseClass.MyBaseClass()构造函数 > 执行MyDerivedClass.MyDerivedClass(int i, int j)构造函数。

其实这里我们可以把使用int i参数的代码几种在MyBaseClass(int i)中，那么在MyDerivedClass(int i, int j)构造函数中要处理的工作就比较少了，只需要处理int j参数。

为此只需要使用构造函数初始化器，它把代码放在方法定义的冒号后面。例如可以在派生类的构造函数定义中指定所使用的基类构造函数，如下所示：

```csharp
public class MyDerviedClass : MyBaseClass
{
	...
	
	public MyDerviedClass(int i, int j) : base(i)
	{
	}
}
```

其中base关键字指定实例化过程使用基类中有指定参数的构造函数。这里通过参数i就会执行我们期望的事件序列。也可以使用这个关键字指定基类构造函数的字面值，如：

```csharp
public class MyDerviedClass : MyBaseClass
{
	...
	
	public MyDerviedClass(int i, int j) : base(5)
	{
	}
}
```

这段代码执行下述序列：

执行System.Object.Object()构造函数 > 执行MyBaseClass.MyBaseClass(int i)构造函数 > 执行MyDerivedClass.MyDerivedClass()构造函数。

除了使用base关键字外，这里还可以将另一个关键字this用作构造函数初始化器。这个关键字指定在调用指定的构造函数前，.NET实例化过程对当前类使用非默认的构造函数。例如：

```csharp
public class MyDerviedClass : MyBaseClass
{
	public MyDerviedClass() : this(5, 6)
	{
	}
	...
	
	public MyDerviedClass(int i, int j) : base(i)
	{
	}
}
```

这段代码将执行下述序列：

执行System.Object.Object()构造函数 > 执行MyBaseClass.MyBaseClass(int i)构造函数 > 执行MyDerivedClass.MyDerivedClass(int i, int j)构造函数 > 执行MyDerivedClass.MyDerivedClass()构造函数。

唯一的限制是使用构造函数初始化器只能指定一个构造函数。但是并不是很严格的限制。我们仍可以构造相当复杂的执行序列。注意在定义构造函数时不要创建无线循环。



## 四、VS和VCE中的OOP工具 ##

这里我们主要看几个工具

* Class View 窗口（类视图）
* 对象浏览器（Object Browser）
* 添加类
* 类图


## 五、类库项目 ##

除了在项目中把类放在不同的文件中之外，还可以把它们放在完全不同的项目中。如果一个项目中只包含类及其他相关类型定义，但没有入口点，该项目就称为类库。

类库项目编译为.dll程序集，在其他项目中添加对类库项目的引用，就可以访问它的内容。这将扩展对象提供的封装性，因为类库可以进行修改和更新，但不会影响用它们的其他项目。你可以方便的升级类提供的服务。

## 六、接口和抽象类 ##

这里介绍创建接口和抽象类，不考虑成员，下一章会重点讲述类的成员。这两种类型很多方面都比较类似。

相似点：抽象类和接口都包含可以由派生类继承的成员，接口和抽象类都不能直接实例化。可以声明这些类型的变量，使用多态性把继承的对象指定给变量，通过变量来使用这些类型的成员，但不能直接访问派生对象的其他成员。

区别：派生类只能继承一个抽象类。相反，类可以使用任意多个接口。抽象类可以拥有抽象成员和非抽象成员。接口成员必须都在使用接口的类上实现，它们没有代码体。另外，接口成员是公共的，但抽象类的成员可以是私有的、受保护的、内部的或受保护内部成员。此外，接口不能包含字段、构造函数、析构函数、静态成员或常量。

## 七、结构类型 ##

前面我们说结构和类很相似，结构是值类型，而类是引用类型。对象在赋给变量时其实是把指针赋给了变量，在操作这个变量时其实是操作这个对象的位置。而结构是值，赋给变量就是把结构自身赋给了变量。这里有一点不同。

## 八、浅度和深度复制 ##

从一个变量到另一个变量按值复制对象，而不是按引用复制对象可能非常复杂。在.NET中考虑这个问题，简单的复制对象可以通过派生于System.Object的MemberwiseClone()方法来完成，这是个受保护的方法，但很容易在对象上定义一个调用该方法的公共方法。这个方法提供的复制称为浅度复制，因为没有考虑引用类型成员。

如果要创建成员的新实例，需要考虑深度复制。通过接口，以标准形式实现它包含的方法。可以以这种处理方式执行所选的任何一个方法体得到这个对象。


