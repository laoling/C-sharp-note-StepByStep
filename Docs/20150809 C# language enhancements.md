本笔记用来记录一些C#改进的语言技术，内容比较丰富。也是初级部分的最后一篇内容。

# C#语言的改进 #

C#语言不是静态不变的，第五版入门书籍主要介绍的是.NET 4版本，而到了第六版C#2012，是C#第五个版本，依赖于.NET4.5版本。从功能角度看，C#以前的版本并不缺乏什么，但并不意味着编程无法进一步简化，或者C#和其他技术之间的关系不能更加流畅。

C#的后续改进也是如此，它们为以前不借助冗长高级编程技术时很难实现的功能提供了新的方式。本笔记就将介绍其中几处改进。

## 一、初始化器 ##

前面介绍过用多种方式实例化和初始化对象。它们都需要在类定义中添加额外代码，以便使用独立的语句来初始化或实例化对象。我们还了解如何创建各种类型的集合类，包含泛型集合类。另外把集合的创建和在集合中添加数据项合并起来并没有什么简便的方法。

对象初始化器提供了一种简化代码的方式，可以合并对象的实例化和初始化。集合初始化器提供了一种简洁的语法，使用一个步骤就可以创建和填充集合。这里就介绍这两个新特性。

#### 1.1 对象初始化器

考虑下面简单类定义：

```csharp
public class Curry
{
	public string MainIngredient {get; set;}
	public string Style {get; set;}
	public int Spiciness {get; set;}
}
```

这个类有三个属性，用前面介绍的自动属性语法来定义。如果希望实例化和初始化这个类的一个对象实例，就必须执行如下几个语句：

```csharp
Curry tastyCurry = new Curry();
tastyCurry.MainIngredient = "panir tikka";
tastyCurry.Style = "jalfrezi";
tastyCurry.Spiciness = 8;
```

如果类定义中未包含构造函数，这段代码就使用C#编译器提供的默认无参数构造函数。为了简化这个初始化过程，可以提供一个合适的非默认构造函数：

```csharp
public class Curry
{
	public Curry(string mainIngredient, string style, int spiciness)
	{
		MainIngredient = mainIngredient;
		Style = style;
		Spiciness = spiciness;
	}

	...
}
```

这样就可以编写代码，把实例化和初始化合并起来：

```csharp
Curry tastyCurry = new Curry("panir tikka", "jalfrezi", 8);
```

这段代码工作的得很好，但它会强制使用Curry类的代码使用这个构造函数，这将阻止前面使用无参数构造函数的代码运行。常常需要提供无参构造函数，在必须序列化类时，情况尤其如此：

```csharp
public class Curry
{
	public Curry()
	{
	}
	
	...
}
```

现在可以使用任意方式实例化和初始化Curry类，但已在最初的类定义中添加几行代码，与提供基本的执行代码相比，这种方法并没有做更多工作。

进入对象初始化器（Object initializer），这是无需在类中添加额外的代码就可以实例化和初始化对象的方式。实例化对象时，要为每个需初始化的、可公开访问的属性或字段使用名称值对，来提供其值。其语法如下：

```csharp
<className> <variableName> = new <className>
{
	<propertyOrField1> = <value1>,
	<propertyOrField2> = <value2>,
	<propertyOrField3> = <value3>,
	...
	<propertyOrFieldN> = <valueN>
};
```

例如重写前面的代码，实例化和初始化Curry类型的一个对象，如下所示：

```csharp
Curry tastyCurry = new Curry
{
	MainIngredient = "panir tikka",
	Style = "jalfrezi",
	Spiciness = 8
};
```

这样的代码我们通常可以放在一行内，而不影响可读性。

使用对象初始化器时，不必显式调用类的构造函数。如果像上述代码那样省略构造函数的括号，就自动调用默认的无参构造函数。这是在初始化器设置参数值之前调用的，以便在需要时为默认构造函数中的参数提供默认值。另外可以调用特定的构造函数。同样先调用这个构造函数，所以在构造函数中对公共属性进行的初始化可能会被初始化器中提供的值覆盖。必须按顺序访问所使用的构造函数（没有显式指出就执行默认构造函数），对象初始化器才能正常工作。

如果要用对象初始化器进行初始化的属性比上面例子中使用的简单类型还复杂，可以使用嵌套的对象初始化器，即使用与前面相同的语法：

```csharp
Curry tastyCurry = new Curry
{
	MainIngredient = "panir tikka",
	Style = "jalfrezi",
	Spiciness = 8,
	Origin = new Restaurant
	{
		Name = "King's Balti",
		Location = "York Road",
		Rating = 5
	}
};
```

注意对象初始化器没有替代非默认的构造函数。在初始化对象时，可以使用对象初始化器来设置属性和字段值，但这并不意味着总是知道需要初始化什么状态。通过构造函数，可以准确地指定对象需要什么值才能起作用，再执行代码，以立即响应这些值。

#### 1.2 集合初始化器

前面介绍过像这样初始化数组：

```csharp
int[] myIntArray = new int[5] {5, 9, 10, 2, 99};
```

这是一种合并实例化和初始化数组的简洁方式。集合初始化器只是把这个语法扩展到集合上：

```csharp
List<int> myIntArray = new List<int> {5, 9, 10, 2, 99};
```

通过合并对象和集合初始化器，就可以用简洁的代码配置集合了。下面的代码：

```csharp
List<Curry> curries = new List<Curry>();
curries.Add(new Curry("Chicken", "Pathia", 6));
curries.Add(new Curry("Vegetable", "Korma", 3));
curries.Add(new Curry("Prawn", "Vindaloo", 9));
```

可以如下替换：

```csharp
List<Curry> moreCurries = new List<Curry>
{
	new Curry
	{
		MainIngredient = "Chicken",
		Style = "Pathia",
		Spiciness = 6
	},
	new Curry
	{
		MainIngredient = "Vegetable",
		Style = "Korma",
		Spiciness = 3
	},
	new Curry
	{
		MainIngredient = "Prawn",
		Style = "Vindaloo",
		Spiciness = 9
	}
};
```

这非常适合于主要用于数据表示的类型，而且，集合初始化和后面介绍的LINQ技术一起使用时效果极佳。

## 二、类型推理 ##

C#是一种强类型化的语言，这表示每个变量都有固定的类型，只能用于接受该类型的代码中。在前面代码中，都有如下形式的代码声明变量：

```csharp
<type> <varName>;

或者

<type> <varName> = <value>;
```

下面代码显示了变量varName的类型：

```csharp
int myInt = 5;
Console.WriteLine(myInt);
```

C#3.0引入了新关键字var，它可以替代前面代码中的type：

```csharp
var <varName> = <value>;
```

在这行代码中，变量`<varName>`隐式地类型化为value的类型。注意，类型的名称并不是var。在下面的代码中：

```csharp
var myVar = 5;
```

那么myVar是int类型的变量，而不是var类型的变量。

这是很重要的一点。使用var时，并不是声明了一个没有类型的变量，也不是声明了一个类型可以变化的变量。否则C#就不再是强类型化的语言了。我们只需利用编译器确定变量的类型即可。

在用var声明变量时，同时必须初始化该变量，因为如果没有初始值，编译器就不能确定变量的类型。下面的代码就不能编译：

```csharp
var myVar;
```

还可以通过数组初始化器来推断数组类型：

```csharp
var myArray = new[] {4, 5, 2};
```

myArray类型被隐式地设置为int[]。在用这种方式隐式指定数组的类型时，初始化器中使用的数组元素必须是以下情形中的一种：
* 相同的类型
* 相同的引用类型或空
* 所有元素的类型都可以隐式的转换为一个类型

如果应用最后一条规则，元素可以转换的类型就称为数组元素的最佳类型。如果这个最佳类型由任何含糊的地方，即所有元素的类型都可以隐式转换为两种或更多类型。代码就不会编译。我们会接收到错误信息，错误中指出没有最佳类型：

```csharp
var myArray = new[] {4, "not an int", 2};
```

还要注意数字值从来都不会解释为可空类型，所有下面的代码无法编译：

```csharp
var myArray = new[] {4, null, 2};
```

但可以使用标准的数组初始化器，如下代码可编译：

```csharp
var myArray = new int?[] {4, null, 2};
```

最后说明一点，标示符var并非不能用于类名。这意味着，如果代码在其作用域中有一个var类，就不能使用var关键字的隐式类型化功能。

类型推理功能本身并不是很有效，因为在前面代码中他只会使事情更复杂。使用var会加大判断给定变量的类型的难度。但是后面会讲，推断类型的概念很重要，因为它是其他技术的基础。下面的匿名类型就是以推断类型为基础的。

## 三、匿名类型 ##

在编写程序一段时间后，会发现我们要花很多时间为数据表示创建简单、乏味的类，在数据库应用程序中尤其如此。常常有一系列类只提供属性。前面写的Curry类就是一个很好的例子：

```csharp
public class Curry
{
	public string MainIngredient {get; set;}
	public string Style {get; set;}
	public int Spiciness {get; set;}
}
```

这个类什么也没有做，只是存储结构化数据。在数据库或电子表格中，可以把这个类看作表中的一行。可以保存这个类的实例的集合类应表示表或电子表格中的多行。

这是类完全可以接受的一种用法，但编写这些类的代码比较单调，对底层数据模式的任何修改都需要添加删除或修改定义类的代码。

匿名类型（anonymous type）是简化这个编程模型的一种方式。其理念是使用C#编译器根据要存储的数据自动创建类型，而不是定义简单的数据存储类型。

可以按如下方式实例化前面的Curry类型：

```csharp
Curry curry = new Curry
{
	MainIngredient = "Lamb",
	Style = "Dhansak",
	Spiciness = 5
};
```

也可以使用匿名类型，如下所示：

```csharp
var curry = new
{
	MainIngredient = "Lamb",
	Style = "Dhansak",
	Spiciness = 5
};
```

这里有两点区别。第一，使用了var关键字。这是因为匿名类型没有可以使用的标识符。稍后可以在IDE看到它们在内部有一个标识符，但不能在代码中使用。第二，在new关键字后面没有指定类型名称，这是编译器确定我们要使用匿名类型的方式。

如果要在数据存储对象中修改属性的值，就不能使用匿名类型。

## 四、动态查找 ##

如上所述，var关键字本身并不是一个类型，所以没有违反C#强类型化的方法论。但在C#4中做了少许改动。C#引入了动态变量的概念，顾名思义动态变量就是类型不固定的变量。

引入动态变量的主要目的是在许多情况下，都希望使用C#处理另一种语言创建的对象。这包括与旧技术的交互操作如COM，以及动态语言处理如Javascript、Python、Ruby。在过去没有非常详细的实现细节，使用C#访问这些语言所创建的对象的方法和属性，需要用到笨拙的语法。

动态查找功能改变了这一切，它允许编写比较简单的代码，但下面会说这个功能是有代价的。

另一个可以使用动态查找功能的情形是处理未知类型的C#对象。这听起来似乎很古怪，但这种情形出现的几率比我们想象的多。如果需要编写一些泛型代码，来处理它接收的输入，这也是一个重要的功能。过去处理这种情形的方法称为反射（reflection），它涉及使用类型信息来访问类型和成员。实际上反射的语法非常类似于上述代码中访问Javascript对象的语法，也非常麻烦。

在后台，动态查找功能是由Dynamic Language Runtime（DLR）动态语言运行库支持。与CLR一样，DLR也是.NET的一部分。这里不做DLR的精确描述和怎么使用来简化交互操作。

#### 4.1 dynamic类型

C#中引入了dynamic关键字，以用于定义变量。例如：

```csharp
dynaminc myDynamicVar;
```

与前面介绍的var关键字不同，的确存在dynamic类型，所以在声明myDynamicVar时，无需直接初始化它的值。

>dynamic类型不同寻常之处，在于它仅在编译期间存在，在运行期间它会被System.Object类型替代。这是比较细微的实现细节，但必须记住这一点，后面会用它澄清些讨论。一旦有了动态变量，就可以继续访问其成员（这里没有列出实际获取变量值的代码）。

```csharp
myDynamicVar.DoSomething("With this!");
```

无论myDynamicVar实际上包含什么值，这行代码都会编译。但是如果所请求的成员不存在，在执行这行代码时就会产生一个RuntimeBinderException类型的异常。

#### 4.2 IDnyamicMetaObjectProvider

在继续之前，应注意如何使用动态类型，或者更确切地讲，在运行期间对成员访问应用某种技术时会发生什么。实际上，有三种不同的方式访问成员：

+ 如果动态值是COM对象，就使用COM技术访问成员（通过IUnknown接口，但这里不需要了解它）。
+ 如果动态值支持IDnyamicMetaObjectProvider接口，就使用该接口访问类型成员。
+ 如果不能使用上述两种技术，就使用反射。

第二种情形比较有趣，它涉及到IDnyamicMetaObjectProvider接口。这里不详细讨论细节，只是注意可以实现这个接口，来准确地控制在运行期间访问成员时会发生什么。

## 五、高级方法参数 ##

C#4扩展了定义和使用方法参数的方式。这主要是为了响应使用外部定义的接口时出现的一个特殊问题，例如Microsoft Office编程模型。其中一些方法有大量的参数，许多参数并不是每次调用都需要的。过去这意味着需要一种方式指定缺失的参数，否则在代码中会出现许多空值：

```csharp
RemoteCall(var1, var2, null, null, null, null, null);
```

也许在理想情况下，这个RemoteCall()方法有多个重载版本，其中一个重载版本需要两个参数：


```csharp
RemoteCall(var1, var2);
```

但是这需要更多带其他参数组合的方法，这本身就会带来更多问题。要维护更多的代码就增加了代码的复杂性等。

VB等语言以另一种方式处理这种情况，即允许使用命名参数和可选参数。在C#4版本中也允许这样做，这是所有.NET语言演化趋于一致的一种方式。

下面介绍这些新的参数类型。

#### 5.1 可选参数

调用方法时，常常给某个参数传送相同的值。例如这可能是一个布尔值，以控制方法操作中的不重要部分。具体而言，考虑下面的方法定义：

```csharp
public List<string> GetWords(string sentence, bool capitalizeWords)
{
	...
}
```

无论给capitalizeWords参数传送什么值，这个方法都会返回一系列string值，每个string值都是输入句子中的一个单词。根据这个方法的使用方式，可能需要把返回的单词列表转换为大写（也许要格式化一个标题）。但在大多数情况下，并不需要这么做，所以大多数调用如下：

```csharp
List<string> words = GetWords(sentence, false);
```

为了把这种方式变成默认方式，可以声明第二个方法，如下：

```csharp
public List<string> GetWords(string sentence)
{
	return GetWords(sentence, false);
}
```

这个方法调用第二个方法，并给capitalizeWords传送值false。

这种方式没有任何错误，但可以想象在使用更多的参数时，这种方式会变得非常复杂。

另一种方式是把capitalizeWords参数变成可选参数。这需要在方法定义中将参数定义为可选参数，这需要提供一个默认值，如果没有提供值，就使用默认值，如下所示：

```csharp
public List<string> GetWords(string sentence, bool capitalizeWords = false)
{
	...
}
```

如果以这种方式定义方法，就能提供一个或两个参数，只有希望capitalizeWords是true时，才需要第二个参数。

###### 5.1.1 可选参数的值

以上方法定义了一个可选参数，其语法如下所示：

```csharp
<parameterType> <parameterName> = <defaultValue>
```

给`<defaultValue>`用作默认值的内容有些限制：默认值必须是字面值、常量值、新对象实例或者默认值类型值。因此不会编译下面的代码：

```csharp
public bool CapitalizationDefault;

public List<string> GetWords(string sentence, bool capitalizeWords = CapitalizationDefault)
{
	...
}
```

为了使上述代码可以工作，CapitalizationDefault值必须定义为常量：

```csharp
public bool CapitalizationDefault = false;
```

这是否有意义取决于具体的情形，在大多数情况下，最好提供一个字面值。

###### 5.1.2 可选参数的顺序

使用可选值时，它们必须位于方法的参数列表末尾。没有默认值的参数不能放在有默认值参数的后面。

因此下面的代码是非法的：

```csharp
public List<string> GetWords(bool capitalizeWords = false, string sentence)
{
	...
}
```

sentence是必选参数，因此必须放在可选参数的前面。

#### 5.2 命名参数

使用可选参数时，可能会发现某个方法有几个可选参数，但您可能只想给第三个参数传递值，从上节的语法看，如果不提供前两个可选参数的值，就无法给第三个可选参数传递值。

C#引入了命名参数（named parameters），它允许指定要使用哪个参数。这不需要在方法定义中进行任何特殊处理，它是一个在调用方法时使用的技术。其语法如下：

```csharp
MyMethod(
	<param1Name>:<param1Value>,
	...
	<paramNName>:<paramNValue>);
```

参数名称是在方法定义中使用的变量名。

只要命名参数存在，就可以用这种方式指定需要的任意多个参数，而且参数的顺序是任意的。命名参数也是可选的。

可以仅给方法调用中的某些参数使用命名参数。当方法签名中有多个可选参数和一些必选参数时，这是非常有用的。可以先指定必选参数，再指定命名的可选参数。例如：

```csharp
MyMethod(
	requiredParameter1Value,
	optionalParameter5: optionalParameter5Value);
```

但注意如果混合使用命名参数和位置参数，就必须先包含所有的位置参数，其后是命名参数。只要使用命名参数，参数的顺序也可以不同。例如：

```csharp
MyMethod(
	optionalParameter5: optionalParameter5Value,
	requiredParameter1: requiredParameter1Value);
```

此时，必须包含所有必选参数的值。

#### 5.3 命名参数和可选参数的规则

自从引入了命名参数和可选参数之后，人们对它们的反应不一，一些开发人员，尤其是使用Microsoft Office的开发人员，非常喜欢它们；但许多其他的开发人员认为这种修改对C#语言而言是不必要的，觉得设计良好的用户界面应该不需要这种访问方式。

使用命名参数和可选参数是有些有点，但过度使用会伤害代码。对于一些情形，如上面提到的Office，它们肯定是有益的。另外像定义了许多选项来控制方法的操作，这使得代码更容易编写和使用。但在大多数情况下，没有合适的理由，就不要使用命名参数和可选参数。如果参数及其用法很明显，就不需要使用命名参数或可选参数来修订代码。

## 六、扩展方法 ##

扩展方法可以扩展类型的功能，但无需修改类型本身。甚至可以使用扩展方法扩展不能修改的类型，包括在.NET Framework中定义的类型。例如使用方法甚至可以给System.String等基本类型添加功能。

为了扩展类型的功能，需要提供可以通过该类型的实例调用的方法。为此创建的方法称为扩展方法（extension method），它可以带任意数量的参数，返回任意类型（包括void）。要创建和使用扩展方法，必须：

1. 创建一个非泛型静态类。
2. 使用扩展方法的语法，给所创建的类添加扩展方法，作为静态方法。
3. 确保使用扩展方法的代码用using语句导入了包含扩展方法类的名称空间。
4. 通过扩展类型的一个实例调用扩展方法，与调用扩展类型的其他方法一样。

C#编译器在第（3）步和第（4）步之间完成了它的使命。IDE会立即发现我们创建了一个扩展方法，并显示在IntelliSense中。

可以通过string对象（这里仅是一个字面量字符串）使用扩展方法MyMarvelousExtensionMethod()。这个方法用一个略微不同的方法图标来表示，该图标包含一个蓝色的向下箭头，这个方法不带其他参数，返回一个字符串。

为了定义扩展方法，应以与其他方法相同的方式定义一个方法，但该方法必须满足扩展方法的语法要求。这些需求如下：

* 方法必须是静态的。
* 方法必须包含一个参数，表示调用扩展方法的类型实例（这个参数这里称为实例参数）。
* 实例参数必须是为方法定义的第一个参数。
* 除了this关键字之外，实例参数不能有其他修饰符。

```csharp
public static class ExtensionClass
{
	public static <ReturnType> <ExtensionMethodName>(this <TypeToExtend instance>)
	{
		...
	}
}
```

导入了包含静态类（其中包括此方法）的名称空间后（也就是使扩展方法变得可用），就可以编写如下代码：

```csharp
<TypeToExtend> myVar;
// myVar is initialized by code not shown here.
myVar.<ExtensionMethodName>();
```

还可以在扩展方法中包含需要的其他参数，并使用其返回类型。

这个调用实际上与下面的调用相同，但语法更简单：

```csharp
<TypeToExtend> myVar;
// myVar is initialized by code not shown here.
ExtensionClass.<ExtensionMethodName>(myVar);
```

另一个优点是，导入后，就可以通过IntelliSense查看匿名类型，这样能更容易地找到需要的功能。扩展方法可能分布在多个扩展类中，甚至分布在多个库中，但它们都会显示在扩展类型的成员列表中。

定义了可以用于某个类型的扩展方法后，还可以把它用于派生于这个类型的子类型。在前面动物示例中，如果Animal类定义了一个扩展方法，就可以在诸如Cow的对象上调用它。

还可以定义在特定接口上执行的扩展方法，接着就可以给实现了该接口的任意类型使用该扩展方法。

扩展方法为在应用程序中重用实用代码库提供了一种方式。他们还可以广泛用于后面介绍的LINQ中。

## 七、Lambda表达式 ##

Lambda表达式是C#3引入的一个结构，可用于简化C#编程的某些方面，尤其是与LINQ合并的方面。Lambda表达式一开始很难掌握，主要是因为其用法非常灵活。Lambda表达式与其他C#语言特性（如匿名方法）结合使用时尤其有用。由于本书后面才介绍LINQ，因此匿名方法是介绍Lambda表达式的最佳切入点。

下面首先概述匿名方法。

#### 7.1 复习匿名方法

上篇笔记介绍过匿名方法，这是提供的内联方法（inline），否则就需要使用委托类型的变量。给事件添加处理程序时，过程如下：

1. 定义一个事件处理方法，其返回类型和参数匹配要订阅的事件需要的委托的返回类型和参数。
2. 声明一个委托类型的变量，用于事件。
3. 把委托变量初始化为委托类型的实例，该实例指向事件处理方法。
4. 把委托变量添加到事件的订阅者列表中。

实际上，这个过程会比上述简单一些，因为一般不使用变量存储委托，只在订阅事件时使用委托的一个实例。

我们前面这样组织代码：

```csharp
Timer myTimer = new Timer(100);
myTimer.Elapsed += new ElapsedEventHandler(WtriteChar);
```

这段代码订阅了Timer对象的Elapsed事件。实际上我们能通过方法组语法，用更少的代码获得相同的结果：

```csharp
myTimer.Elapsed += WriteChar;
```

C#编译器知道Elapsed事件需要的委托类型，所以可以填充该类型。但是在大多数情况下，最好不要这么做，因为这会使代码更难理解，也不清楚会发生什么。使用匿名方法时，该过程会减少为一步：

>使用内联的匿名方法，该匿名方法的返回值和参数匹配所订阅事件需要的委托的返回类型和参数。

用delegate关键字定义内联的匿名方法：

```csharp
myTimer.Elapsed +=
	delegate(object source, ElapsedEventArgs e)
	{
		Console.WriteLine("Event handler called after {0} milliseconds.", (source as Timer).Interval);
	};
```

这段代码可以正常工作，且使用了事件处理程序。主要区别是这里使用的匿名方法对于其余代码而言实际上是隐藏的。例如不能在应用程序的其他地方重用这个事件处理程序。另外为了更好地加以描述，这里使用得语法有点沉闷，delegate关键字总是会带来混淆，因为它有双重含义——匿名方法和定义委托类型都要使用它。

#### 7.2 把Lambda表达式用于匿名方法

下面看看Lambda表达式。Lambda表达式是简化匿名方法的语法的一种方式。实际上Lambda表达式还有其他用处，但为了简单起见，本节只介绍Lambda表达式的这个方面。使用Lambda表达式可以重写上一节最后一段代码，如下所示：

```csharp
myTimer.Elapsed += (source, e) => Console.WriteLine(
	"Event handler called after {0} milliseconds.",
	 (source as Timer).Interval);
```

这段代码初看上去还好，只是有点让人不明白其具体意思。但是如果仔细观察，就会看出代码是如何工作的。它与所替代的匿名方法有什么关系。Lambda表达式由3个部分组成：

* 放在括号中的参数列表（未类型化）
* =>运算符
* C#语句

使用前面的匿名类型一节介绍的逻辑，从上下问推断出参数的类型。=>运算符只是把参数列表与表达式分开。在调用Lambda表达式时，执行表达式体。

编译器会提取这个Lambda表达式，创建一个匿名方法，其工作方式与上一节中的匿名方法相同。其实，它会被编译为相同或相似的CIL代码。

#### 7.3 Lambda表达式的参数

在前面代码中，Lambda表达式使用类型推理功能确定所传送的参数类型。实际上这不是必须的，也可以定义类型。例如，可以使用下面的Lambda表达式：

```csharp
(int paramA, int paramB) => paramA + paramB
```

其优点是代码更易理解，但不够简单灵活。在前面委托类型的示例中，可以通过隐式类型化的Lambda表达式来使用其他数字类型，例如long变量。

注意，不能在同一个Lambda表达式中同时使用隐式和显式的参数类型。下面的Lambda表达式就不会编译，因为paramA是显式类型化的，而paramB是隐式类型化的：

```csharp
(int paramA, paramB) => paramA + paramB
```

还可以定义没有参数的Lambda表达式，这使用空括号来表示：

```csharp
() => Math.PI
```

当委托不需要参数时，但需要返回一个double值时，就可以使用这个Lambda表达式。

#### 7.4 Lambda表达式的语句体

在前面所有代码中，Lambda表达式的语句都是只使用了一个表达式。并说明了这个表达式如何解释为Lambda表达式的返回值，例如，如何给返回类型为int的委托使用表达式paramA+paramB作为Lambda表达式的语句体（假定paramA和paramB隐式或显式类型化为int值，如示例代码所示）。

前面的一个示例说明了对于语句体中使用的代码而言，返回类型为void的委托的要求并不高：

```csharp
myTimer.Elasped += (source, e) => Console.WriteLine(
	"Event handler called after {0} milliseconds.", (source as Timer).interval);
```

上面的语句不返回任何值，所以它只是执行，其返回值不在任何地方使用。

Lambda表达式可以看作匿名方法语句的扩展，所以还可以在Lambda表达式的语句体中包含多个语句。为此只需要把一个代码块放在花括号中，类似于C#提供多行代码的其他情况：

```csharp
(param1, param2) => 
{
	//Multiple statements ahoy!
}
```

如果使用Lambda表达式和返回类型不是void的委托类型，就必须用return关键字返回一个值，这与其他方法一样：

```csharp
(param1, param2) => 
{
	//Multiple statements ahoy!
	return returnValue;
}
```

例如可以把前面示例中的如下代码：

```csharp
PerformOperations((paramA, paramB) => paramA + paramB);
```

改写为：

```csharp
PerformOperations(delegate(int paramA, int paramB)
	{
		return paramA + paramB;
	});
```

另外，也可以把代码改写为：

```csharp
PerformOperations((paramA, paramB) =>
	{
		return paramA + paramB;
	});
```

这更像是原来的代码，因为它包含paramA和paramB参数的隐式类型化。

在大多数情况下，使用单一的表达式时，大都使用Lambda表达式，它们肯定是最简洁的。说实话，如果需要多个语句，则定义一个非匿名方法来替代Lambda表达式比较好，这也会使代码更便于重用。

#### 7.5 Lambda表达式用作委托和表达式树

#### 7.6 Lambda表达式和集合

