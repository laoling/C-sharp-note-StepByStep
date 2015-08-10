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

## 三、匿名推理 ##

## 四、动态查找 ##

#### 4.1 dynamic类型

#### 4.2 IDnyamicMetaObjectProvider

## 五、高级方法参数 ##

#### 5.1 可选参数

###### 5.1.1 可选参数的值

###### 5.1.2 可选参数的顺序

#### 5.2 命名参数

#### 5.3 命名参数和可选参数的规则

## 六、扩展方法 ##

## 七、Lambda表达式 ##

#### 7.1 复习匿名方法

#### 7.2 把Lambda表达式用于匿名方法

#### 7.3 Lambda表达式的参数

#### 7.4 Lambda表达式的语句体

#### 7.5 Lambda表达式用作委托和表达式树

#### 7.6 Lambda表达式和集合

