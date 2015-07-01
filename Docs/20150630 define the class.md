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

