本篇笔记继续介绍C#的OOP技术，都是前面没有涉及到的技术，这里我们讨论一下。

# 其他OOP技术 #

## 一、::运算符和全局命名空间限定符 ##

::运算符提供了另一种访问命名空间中类型的方法。如果要使用一个命名空间的别名，但该别名与实际命名空间层次结构之间的界限不清晰，这将是必要的。在那种情况下，命名空间层次结构优先于命名空间别名。为了阐明其含义，考虑下面的代码：

```csharp
using MyNamespaceAlias = MyRootNamespace.MyNestedNamespace;

namespace MyRootNamespace
{
	namespace MyNamespaceAlias
	{
		public class MyClass
		{
		}
	}

	namespace MyNestedNamespace
	{
		public class MyClass
		{
		}
	}
}
```

MyRootNamespace中的代码使用下面的代码引用一个类：

```csharp
MyNamespaceAlias.MyClass
```

这行代码表示的类是MyRootNamespace.MyNamespaceAlias.MyClass类，而不是MyRootNamespace.MyNestedNamespace.MyClass类。也就是说，MyRootNamespace.MyNamespaceAlias命名空间隐藏了由using语句定义的别名，该别名指向MyRootNamespace.MyNestedNamespace命名空间。仍然可以访问这个命名空间以及其中包含的类，但需要使用不同的语法：

```csharp
MyNestedNamespace.MyClass
```

另外，还可以使用::运算符：

```csharp
MyNamespaceAlias::MyClass
```

使用这个运算符会迫使编译器使用由using语句定义的别名，因此代码指向MyRootNamespace.MyNestedNamespace.MyClass。

::运算符还可以和global关键字一起使用，它实际上是顶级根命名空间的别名。这有助于更清晰地说明要指向哪个命名空间，如下所示：

```csharp
global::System.Collections.Generic.List<int>
```

这是希望使用的类，即`List<T>`泛型集合类。他肯定不是用下面代码定义的类：

```csharp
namespace MyRootNamespace
{
	namespace System
	{
		namespace Collections
		{
			namespace Generic
			{
				class List<T>
				{
				}
			}
		}
	}
}
```

当然，应避免使用命名空间的名称与已有的.NET命名空间相同，但这个问题只会在大型项目中才会出现，尤其是作为大型开发队伍中的一员进行开发时，此类问题就更严重。使用::运算符和global关键字可能是访问所需类型的唯一方式。

## 二、定制异常 ##

前面我们讨论过异常，以及如何使用try...catch...finally块处理它们。我们还讨论了几个标准的.NET异常，包括异常的基类System.Exception。在应用程序中，有时也可以从这个基类中派生自己的异常类，并使用它们，而不是使用标准的异常。这样就可以把更具体的信息发送给捕获该异常的代码，让处理异常的捕获代码更有针对性。例如，可以给异常类添加一个新属性，以便访问某些底层信息，这样异常的接收代码就可以做出必要的改变，或者仅给出异常起因的更多信息。

定义了异常类后，就可以使用调试|Exceptions对话框中的Add按钮，把它添加到可以识别的异常列表中，然后定义与异常相关的操作，如前面所示。

> 注意：在System命名空间中有两个基本的异常类ApplicationException和SystemException，它们派生于Exception。System.Exception用作.NET预定义的异常的基类，ApplicationException由开发人员用于派生自己的异常类。但最近的最佳实践方式不是从这个类中派生异常，而是直接使用Exception。ApplicationException类在未来可能被废弃。

## 三、事件 ##

我们这里讨论OOP技术中最常用的一种技术：事件。我们先介绍基础，分析事件到底是什么。再讨论几个简单的事件，看看它可以做什么。然后论述如何创建和使用自己的事件。

#### 3.1 事件的含义

事件类似于异常，因为它们都是由对象引发（抛出），我们可以提供代码来处理事件。但它们也有几个重要区别。最重要的区别是没有与try...catch类似的结构来处理事件，而必须订阅(subscribe)它们。订阅一个事件的含义是提供代码，在事件发生时执行这些代码，它们称为事件处理程序。

单个事件可供多个处理程序订阅，在该事件发生时，这些处理程序都会被调用，其中包含引发该事件的对象所在的类中的事件处理程序，但事件处理程序也可能在其他类中。

事件处理程序本身都是简单的方法。对事件处理方法的唯一限制是它必须匹配于事件所要求的返回类型和参数。这个限制是事件定义的一部分，由一个委托指定。

基本处理过程如下所示：

* 首先，应用程序创建一个可以引发事件的对象。

	例如假定一个即时消息传送（instant messaging）应用程序创建的对象表示一个远程用户的连接。当接收到通过该连接从远程用户送来的消息时，这个连接对象会引发一个事件。

* 应用程序订阅事件。

	为此，即时消息传送应用程序将定义一个方法，该方法可以与事件指定的委托类型一起使用，把这个方法的一个引用传送给事件，而事件的处理方法可以是另一个对象的方法，假定是表示显示设备的对象，当接收到信息时，该方法将显示即时消息。

* 引发事件后，就通知订阅器。

	当接收到通过连接对象传来的即时消息时，就调用显示设备对象上的事件处理方法。因为我们使用的是一个标准方法，所以引发事件的对象可以通过参数传送任何相关的信息，这样就大大增加了事件的通用性。在本例中，一个参数是即时消息的文本，事件处理程序可以在显式设备对象上显示它。

#### 3.2 处理事件

如前所述，要处理事件，需要提供一个事件处理方法来订阅事件，该方法的返回类型和参数应该匹配事件指定的委托。

这里我们演示一个使用简单计时器对象引发事件并调用一个处理方法，见Chapter13Example01.

#### 3.3 定义事件

接着论述如何定义和使用自己的事件。

我们将使用前面介绍的即时消息传送应用程序示例，并创建一个Connection对象，该对象引发由Display对象处理的事件。见Chapter13Example02.

##### 3.3.1 多用途的事件处理程序

前面Timer.Elapsed事件的委托包含了事件处理程序中常见的两类参数，如下所示：

* object source —— 引发事件的对象的引用
* ElapsedEventArgs e —— 由事件传递的参数

在这个事件（以及许多其他的事件）中使用Object类型参数的原因是，我们常常要为由不同对象引发的几个相同事件使用同一个事件处理程序，但仍要指定哪个对象生成了事件。

为了说明这点，我们写一个例子，见Chapter13Example03.

##### 3.3.2 EventHandle和泛型EventHandler<T>类型

大多数情况下，都应遵循上一节提出的模式，使用返回类型为void、带两个参数的事件处理程序。第一个参数的类型是object，是事件源。第二个参数的类型派生于System.EventArgs，包含任意事件变元。这非常常见，所以.NET提供了两个委托类型`EventHandler`和`EventHandler<T>`，以便于定义事件。它们都是委托，使用标准的事件处理模式。泛型版本允许指定要使用的事件变元的类型。

所以，在前面的示例中，可以不定义自己的MessageHandler泛型类型，而是定义MessageArrived事件，如下所示：

```csharp
public class Connection
{
	public event EventHandler MessageArrived;

	...
}
```

或者

```csharp
public class Connection
{
	public event EventHandler<MessageArrivedEventArgs> MessageArrived;

	...
}
```

这显然是好事，因为代码被简化了。

##### 3.3.3 返回值和事件处理程序

前面的所有事件处理程序都使用void类型的返回值。可以为事件提供返回类型，但这会出问题。这是因为引发给定的事件，可能会调用好几个事件处理程序。如果这些处理程序都返回一个值，那么我们该使用哪个返回值？

系统处理这个问题的方式是，只允许访问由事件处理程序最后返回的那个值，也就是最后一个订阅该事件的处理程序返回的值。这个功能在某些情况下是有用的，但最好使用void类型的事件处理程序，且避免使用out类型的参数。使用out类型的参数，参数返回值的源头就是不清楚的。

##### 3.3.4 匿名方法

除了定义事件处理方法之外，还可以使用匿名方法(anonymous method)。匿名方法实际上并非传统意义上的方法，它不是某个类上的方法，而纯粹是为用作委托目的而创建的。

要创建匿名方法，需要使用下面的代码：

```csharp
delegate(parameters)
{
	// Anonymous method code.
};
```

其中parameters是一个参数列表，这些参数匹配正在实例化的委托类型，由匿名方法的代码使用，例如：

```csharp
delegate(Connection source, MessageArrivedEventArgs e)
{
	// Anonymous method code matching MessageHandler event.
};
```

使用这段代码可以完全绕过例子中的DisplayMessage()方法：

```csharp
myConnection1.MessageArrived += 
	delegate(Connection source, MesssageArrivedEventArgs e)
	{
		Console.WriteLine("Message arrived from: {0}", source.Name);
		Console.WriteLine("Message Text: {0}", e.Message);
	};
```

对于匿名方法要注意，对于包含它们的代码块来说，它们是局部的，可以访问这个区域内的局部变量。如果使用这样一个变量，它就成为外部变量(outer variable)。外部变量在超出作用域时，是不会删除的，这与其他局部变量不同，在使用它们的匿名方法被销毁时，外部变量才会删除。这比我们希望的时间晚一些，所以要格外小心。如果外部变量占用了大量内存，或者使用的资源在其他方面是比较昂贵的，极可能导致内存或性能问题。