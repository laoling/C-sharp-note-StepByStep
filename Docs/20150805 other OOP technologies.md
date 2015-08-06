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

#### 3.1 事件的含义

#### 3.2 处理事件

#### 3.3 定义事件

##### 3.3.1 多用途的事件处理程序

##### 3.3.2 EventHandle和泛型EventHandler<T>类型

##### 3.3.3 返回值和事件处理程序

##### 3.3.4 匿名方法

