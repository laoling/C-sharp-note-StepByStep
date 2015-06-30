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