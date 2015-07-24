这一章笔记我们主要整理一些OOP实现过程中的一些技巧，主要包括集合、字典、迭代器、类型比较、值比较等内容。本章的实例也比较多，更是要将我们创建的CardLib库进行多次优化。

# 集合、比较、转换 #

## 一、集合 ##

System.Collections命名空间中的几个接口提供了基本的集合功能：

* IEnumerable可以迭代集合中的项。
* ICollection（继承于IEnumerable）可以获取集合中的项的个数，并能把项复制到一个简单的数组类型中。
* IList（继承于IEnumerable和ICollection）提供了集合的项列表，允许访问这些项，并提供其他的一些与项列表相关的基本功能。
* IDictionary（继承于IEnumerable和ICollection）类似于IList，但提供了可通过键值（而不是索引）访问的项列表。

System.Array实现了IEnumerable、ICollection、IList，但不支持IList的一些更高级的功能，它表示大小固定的项列表。

#### 1、使用集合

System.Collections命名空间中的类System.Collection.ArrayList也实现了IList、ICollection和IEnumerable接口，但实现方式比System.Array更复杂。数组的大小是固定的，而这个类可以用于表示大小可变的项列表。

#### 2、定义集合

下面我们讨论如何创建自己的、强类型化的集合。

一种方法是手动执行需要的方法。费时且难于实现。所有我们很少使用这种方法。

另一种方法是从一个类中派生自己的集合，例如System.Collection.CollectionBase类，这个抽象类提供了很多集合类的实现方式。这就是推荐的方法。

Collection.Base类有接口IEnumerable、ICollection和IList，但只提供了一些需要的实现代码，特别是IList的Clear()和RemoveAt()方法，以及ICollection的Count属性。如果要使用提供的功能，就需要自己执行其他代码。

为便于完成任务，CollectionBase提供了两个受保护的属性，他们可以访问存储的对象本身。我们可以使用List和InnerList，List可以通过IList接口访问项，InnerList则是用于存储项的ArrayList对象。

#### 3、索引符

索引符（indexer）是一种特殊类型的属性，可以把它添加到一个类中，以提供类似于数组的访问。实际上可以通过索引符提供更复杂的访问，因为我们可以使用方括号语法定义和使用复杂的参数类型。最常见的用法是对项执行简单的数字索引。

在Animal对项的Animals集合中添加一个索引符，如下所示：

```csharp
public class Animals : CollectionBase
{
	...
	public Animal this[int animalIndex]
	{
		get
		{
			return (Animal)List[animalIndex];
		}
		set
		{
			List[animalIndex] = value;
		}
	}
}
```

this关键字与方括号中的参数一起使用，但这看起来类似于其他属性。这个语法是合理的，因为在访问索引符时，将使用对象名，后跟放在方括号中的索引参数（例如MyAnimals[0]）。

这里对List属性使用一个索引符，这里需要进行显式数据转换，因为IList.List属性返回了一个System.Object对象。`return (Animal)List[animalIndex];`  注意，我们为这个索引符定义了一个类型。使用该索引符访问某项时，就可以得到这个类型。这种强类型化功能意味着，可以编写下面的代码：

```csharp
animalCollection[0].Feed();
```

而不是

```csharp
((Animal)animalCollection[0]).Feed();
```

这是强类型化的定制集合的另一个方便特性。

#### 4、关键字值集合和IDictionary

除了IList接口外，集合还可以实现类似的IDictionary接口，允许项通过关键字值（如字符串名）进行索引，而不是通过一个索引。这也可以使用索引符来完成，但这次的索引符参数是与存储的项相关联的关键字，而不是int索引，这样集合就更便于用户使用了。

与索引的集合一样，可以使用一个基类简化IDictionary接口的实现，这个基类就是DictionaryBase，它也实现IEnumerable和ICollection，提供了对任何集合都相同的基本集合处理功能。

DictionaryBase 与 CollectionBase一样，实现通过其支持的接口获得的一些成员。DictionaryBase也实现Clear和Count成员，但不实现RemoveAt()。这是因为这是IList中的一个方法，不是IDictionary接口中的一个方法。但是，IDictionary有一个Remove()方法，这是一个应在基于DictionaryBase的定制集合类上实现的方法。

下面是Animals类的另一个版本，这次派生于DictionaryBase。包括了Add()、Remove()和一个通过关键字访问的索引符的实现代码：

```csharp
public class Animals : DictionaryBase
{
	public void Add(string newID, Animal newAnimal)
	{
		Dictionary.Add(newID, newAnimal);
	}
	
	public void Remove(string animalID)
	{
		Dictionary.Remove(animalID);
	}

	public Animals()
	{
	}

	public Animal this[string animalID]
	{
		get
		{
			return (Animal)Dictionary[animalID];
		}
		set
		{
			Dictionary[animalID] = value;
		}
	}
}
```

基于DictionaryBase的集合和基于CollectionBase的集合之间的另一个区别是foreach的工作方式略有区别。集合可以直接从集合中提取Animal对象。使用foreach和DictionaryBase派生类可以提供DictionaryEntry结构，这是在System.Collections命名空间中定义的一个类型。要得到Animal对象本身，就必须使用这个结构的Value成员，也可以使用结构的Key成员得到相关的关键字。要使代码等价于前面的代码：

```csharp
foreach (Animal myAnimal in animalCollection)
{
	Console.WriteLine("New {0} object added to custom collection Name = {1}",
						myAnimal.ToString(), myAnimal.Name());
}

//变换
foreach (DictionaryEntry myEntry in animalCollection)
{
	Console.WriteLine("New {0} object added to custom collection Name = {1}",
						myEntry.Value.ToString(), ((Animal)myEntry.value).Name);
}
```

#### 5、迭代器

IEnumerable接口负责使用foreach循环，但重写foreach循环或实现方法并不容易。比如，在foreach循环中迭代集合collectionObject的过程如下：

1）调用collectionObject.GetEnumerator(),返回一个IEnumerator引用。这个方法可以通过IEnumeratable接口的实现代码来获得，但这是可选的。 

2）调用所返回的IEnumerator接口的MoveNext()方法。

3）如果MoveNext()方法返回true，就使用IEnumerator接口的Current属性获取对象的一个引用，用于foreach循环。

4）重复前面两步，直到MoveNext()方法返回false为止，此时循环停止。

所以为了在类中进行这些操作，必须重写几个方法，跟踪索引，维护Current属性，以及执行其他一些操作。

一个简单的替代方法是使用迭代器。使用迭代器可以有效地在后台生成许多代码，正确的完成所有任务。而且迭代器语法非常容易。

迭代器的定义是，它是一个代码块，按顺序提供了要在foreach循环中使用的所有值。一般情况下，这个代码块是一个方法，也可以使用属性访问器或其他代码作为迭代器。简单起见，这里仅介绍方法。

无论代码是什么，其返回类型都是有限制的。与期望正好相反，这个返回类型与所枚举的对象类型不同。例如在表示Animal对象集合的类中，迭代器块的返回类型不可能是Animal。两种可能的返回类型是前面提到的接口类型IEnumerable和IEnumerator。使用这两种类型的场合是：

+ 如果要迭代一个类，可使用方法GetEnumerator()，其返回类型是IEnumerator。
+ 如果要迭代一个类成员，例如一个方法，则使用IEnumerable。

在迭代器块中，使用yield关键字选择要在foreach循环中使用的值。其语法如下：

```csharp
yield return value;
```

看下面简单例子：

```csharp
public static IEnumerable SimpleList()
{
	yield return "string 1";
	yield return "string 2";
	yield return "string 3";
}

public static void Main(string[] args)
{
	foreach (string item in SimpleList())
		Console.WriteLine(item);

	Console.ReadKey();
}
```

显然这个迭代器不是特别有用，但它允许查看执行过程，了解实现代码有多简单。实际上代码返回了object类型的值，因为object是所有类型的基类，也就是说可以从yield语句返回任意类型。

可以使用下面的语句中断信息返回foreach循环的过程：

```csharp
yield break;
```

【迭代器和集合】

介绍迭代器如何用于迭代存储在字典类型的集合中的对象，无需处理DictionaryItem对象。下面是集合类Animals：

```csharp
public class Animals : DictionaryBase
{
	public void Add(string newID, Animal newAnimal)
	{
		Dictionary.Add(newID, newAnimal);
	}

	public void Remove(string animalID)
	{
		Dictionary.Remove(animalID);
	}

	public Animals()
	{
	}

	public Animal this[string animalID]
	{
		get
		{
			return (Animal)Dictionary[animalID];
		}
		set
		{
			Dictionary[animalID] = value;
		}
	}
}
```

可以在这段代码中添加如下简单的迭代器，以便执行预期的操作：

```csharp
//Animals.cs
public new IEnumerator GetEnumerator()
{
	foreach (object animal in Dictionary.Values)
		yield return (Animal)animal;
}
```

现在可以使用下面的代码迭代集合中的Animal对象了：

```csharp
//Program.cs
foreach (Animal myAnimal in animalCollection)
{
	Console.WriteLine("New {0} object added to custom collection, " + 
					  "Name = {1}", myAnimal.ToString(), myAnimal.Name);
}
```

#### 6、深复制

前面介绍了使用受保护的方法System.Object.MemberwiseClone()进行浅复制。但通过引用类型的多次引用后，会造成克隆结果的改变。要解决这个问题，就需要执行深复制。

深复制在.NET Framework的标准方式：实现ICloneable接口，该接口有一个方法Clone();这个方法不带参数，返回一个object类型的结果，其签名和原来使用的GetCopy()方法相同。

eg：使用深复制的代码：

```csharp
public class Content
{
	public int Val;
}

public Content MyContent = new Content();

public Cloner(int newVal)
{
	MyContent.Val = newVal;
}

public object Clone()
{
	Cloner clonedCloner = new Cloner(MyContent.Val);
	return clonedCloner;
}
```

其中使用包含在源Cloner对象中的Content对象（MyContent）的Val字段，创建一个新Cloner对象。这个字段是一个值类型，所以不需要深复制。

注意：在比较复杂的对象系统中，调用Clone()是一个递归过程。例如，如果对象Cloner类的MyContent字段也需要深复制，就要像下面这样使用：

```csharp
public class Cloner : ICloneable
{
	public Content MyContent = new Content();

	...

	public object Clone()
	{
		Cloner clonedCloner = new Cloner();
		clonedCloner.MyContent = MyContent.Clone();
		return ClonedCloner;
	}
}
```

这里调用了默认的构造函数，以便简化创建一个新Cloner对象的语法。为了使这段代码能正常工作，还需要在Content类上实现ICloneable接口。

## 二、比较 ##

在C#编程中，把对象传给方法时，下一步要进行的操作常常取决于对象的类型。

#### 1、类型比较

在比较对象时，常常需要了解它们的类型，才能确定是否可以进行值的比较。第九章GetType()方法，所有的类都从System.Object中继承了这个方法，这个方法和typeof()运算符一起使用，就可以确定对象的类型：

```csharp
if (myObj.GetType() == typeof(MyComplexClass))
{
	// myObj is an instance of the class MyComplexClass.
}
```

前面还提到ToString()的默认实现方式，ToString()也是从System.Object继承来的，该方法可以提供对象类型的字符串表示。也可以比较这些字符串，但这是比较杂乱的方式。

本节将介绍一种比较值的简便方式：is运算符。它可以提供可读性比较高的代码，还可以检查基类，在介绍is运算符之前，需要了解处理值类型时后台的一些常见操作：封箱和拆箱（boxing and unboxing）。

###### 1）封箱和拆箱

封箱(boxing)是把值类型转换为System.Object类型，或者转换为由值类型实现的接口类型。拆箱(unboxing)是相反的转换过程。

eg：下面的结构类型

```csharp
struct MyStruct
{
	public int Val;
}
```

可以把这种类型的结构放在object类型的变量中，以封箱它：

```csharp
MyStruct valType1 = new MyStruct();
valType1.Val = 5;
object refType = valType1;
```

其中创建了一个类型为MyStruct的新变量，并把一个值赋予这个结构的Val成员，然后把它封箱在object类型的变量中。

以这种方式封箱变量而创建的对象，包含值类型变量的一个副本的引用，而不包含源值类型变量的引用。要进行验证，可以修改源结构的内容，把对象中包含的结构拆箱到新变量中，检查其内容：

```csharp
valType1.Val = 6;
MyStruct ValType2 = (MyStruct)refType;
Console.WriteLine("ValType2.Val = {0}", valType2.Val);
```

执行这段代码将得到如下输出结果：

> valType2.Val = 5

但在把一个引用类型赋予对象时，将执行不同的操作。把MyStrut改为一个类（不考虑类名不合适的情况），即可看到这种情形：

```csharp
class MyStruct
{
	public int Val;
}
```

再次忽略名称错误的变量，就会得到下面结果：

> ValType2.Val = 6;

也可以把值类型封箱到一个接口类型中，只要它们实现这个接口即可。例如，假定MyStrut类型实现IMyInterface接口，如下所示：

```csharp
interface IMyInterface
{
}

struct MyStruct : IMyInterface
{
	public int Val;
}
```

接着把结构封箱到一个IMyInterface类型中，如下：

```csharp
MyStruct valType1 = new MyStruct();
IMyInterface refType = valType1;
```

然后使用一般的数据类型转换语法拆箱它：

```csharp
MyStruct ValType2 = (MyStruct)refType;
```

从这些示例可以看出封箱是在没有用户干涉的情况下进行的（即不需要编写任何代码），但拆箱一个值需要进行显式转换，即需要进行数据类型转换（封箱是隐式的，所以不需要进行数据类型转换）。

封箱有用的原因有二：

* 它允许在项的类型是object的集合中使用值类型，如ArrayList。
* 有一个内部机制允许在值类型上调用object，例如int和结构。

最后注意：在访问值类型内容前，必须进行拆箱。

###### 2）is运算符

is运算符并不是说明对象是某种类型的一种方式，而是可以检查对象是否是给定类型，或者是否可以转换为给定类型，如果是这个运算符就返回true。

is运算符的语法如下：

```csharp
<operand> is <type>
```

这个表达式的结果如下：

* 如果<type>是一个类类型，而<operand>也是该类型，或者它继承了该类型，或者它可以封装到该类型中，则结果为true。
* 如果<type>是一个接口类型，而<operand>也是该类型，或者它继承了该类型，或者它可以封装到该类型中，则结果为true。
* 如果<type>是一个值类型，而<operand>也是该类型，或者它可以拆箱到该类型中，则结果为true。

#### 2、值比较

考虑两个表示人的Person对象，它们都有一个Age整型属性。下面要比较它们，看看哪个人年龄较大。为此可以使用一下代码：`if (person1.Age > person2.Age){...}`这是可以的，还有其他方法，例如，使用下面的语法：`if (person1 > person2){...}`。可以使用运算符重载，这是一个强大的技术，但使用需慎重。因为这段代码中年龄的比较并不是很明显，该代码还可以比较身高、体重、IQ等。

另一个方法是使用IComparable和IComparer接口，它们可以用标准的方式定义比较对象的过程。这是由.NET Framework中各种集合类提供的方式，是对集合中的对象进行排序的一种绝佳方式。

###### 1）运算符重载

运算符重载（operator overloading）可以对我们设计的类使用标准的运算符，例如+、>等。这称为重载。因为在使用特定的参数类型时，我们为这些运算符提供了自己的实现代码，其方式与重载方法相同，也是为同名的方法提供不同的参数。运算符重载非常有用，因为我们可以在运算符重载的实现中执行需要的任何操作。

我们先看运算符重载的基本语法。要重载运算符，可给类添加运算符类型成员（它们必须是static）。一些运算符有多种用途，因此我们还指定了要处理多少个操作数，以及这些操作数的类型。一般情况下，操作数的类型与定义运算符的类相同，但也可以定义处理混合类型的运算符。

eg：考虑一个简单类型AddClass1，如下

```csharp
public class AddClass1
{
	public int val;
}
```

这仅是int值的一个包装器（wrapper），但可以用于说明原理。对于这个类，下面的代码不能编译：

```csharp
AddClass1 op1 = new AddClass1();
op1.val = 5;
AddClass1 op2 = new AddClass1();
op2.val = 5;
AddClass1 op3 = op1 + op2;
```

其错误是 + 运算符不能应用于AddClass1类型的操作数，因为我们尚未定义要执行的操作。下面的代码则可执行，但得不到预期的效果：

```csharp
AddClass1 op1 = new AddClass1();
op1.val = 5;
AddClass1 op2 = new AddClass1();
op2.val = 5;
bool op3 = op1 == op2;
```

其中使用二元运算符来比较op1和op2，看他们是否引用的同一对象，而不是验证它们的值是否相等。

要重载 + 运算符，可使用下述代码：

```csharp
public class AddClass1
{
	public int val;

	public static AddClass1 operator +(AddClass1 op1, AddClass1 op2)
	{
		AddClass1 returnVal = new AddClass1();
		returnVal.val = op1.val + op2.val;
		return returnVal;
	}
}
```

可以看出运算符重载看起来与标准静态方法声明类似，但它们使用关键字operator和运算符本身，而不是一个方法名。现在可以成功地使用+运算符和这个类，如上面的实例所示：

```csharp
AddClass1 op3 = op1 + op2;
```

重载所有二元运算符都一样，一元运算符看起来也类似，但只有一个参数：

```csharp
public class AddClass1
{
	public int val;

	public static AddClass1 operator +(AddClass1 op1, AddClass1 op2)
	{
		AddClass1 returnVal = new AddClass1();
		returnVal.val = op1.val + op2.val;
		return returnVal;
	}

	public static AddClass1 operator -(AddClass1 op1)
	{
		AddClass1 returnVal = new AddClass1();
		returnVal.val = -op1.val;
		return returnVal;
	}
```

这两个运算符处理的操作数的类型与类相同，返回值也是该类型，但考虑下面的类定义：

```csharp
public class AddClass1
{
	public int val;

	public static AddClass1 operator +(AddClass1 op1, AddClass1 op2)
	{
		AddClass1 returnVal = new AddClass1();
		returnVal.val = op1.val + op2.val;
		return returnVal;
	}

	public class AddClass2
	{
		public int val;
	}

	public class AddClass3
	{
		public int val;
	}
```

下面的代码就可以执行：

```csharp
AddClass1 op1 = new AddClass1();
op1.val = 5;
AddClass2 op2 = new AddClass1();
op2.val = 5;
AddClass3 op3 = op1 + op2;
```

这种混合类型使用时可以考虑。但要注意如果把相同的运算符加到AddClass2中，上面的代码就会失败，因为它弄不清要使用哪个运算符。因此，应注意不要把签名相同的运算符添加到多个类中。

还要注意如果混合了类型，操作数的顺序必须与运算符重载的参数顺序相同。如果使用了重载的运算符和顺序错误的操作数，操作就会失败。所以不能像下面这样使用运算符：

```csharp
AddClass3 op3 = op2 + op1;
```

当然除非提供了另一个重载运算符和倒序的参数：

```csharp
public static AddClass3 operator +(AddClass2 op1, AddClass1 op2)
{
	AddClass3 returnVal = new AddClass3();
	returnVal.val = op1.val + op2.val;
	return returnVal;
}
```

可以重载的运算符：

* 一元运算符： + - ! ~ ++ -- true false
* 二元运算符： + - * / % & | ^ << >>
* 比较运算符： == != < > <= >=

###### 2）IComparable和IComparer接口

IComparable和IComparer接口是.NET Framework中比较对象的标准方式。这两个接口之间的差别如下：

+ IComparable在要比较的对象的类中实现，可以比较该对象和另一个对象。
+ IComparer在一个单独的类中实现，可以比较任意两个对象。

一般使用IComparable给出类的默认比较代码，使用其他类给出非默认的比较代码。

IComparable提供了一个方法CompareTo()，这个方法接受一个对象。

eg：实现可以为实现方法传送一个Person对象，以便确定这个人是否比当前的人更年老还是更年轻。实际上这个方法返回一个int，所以也可以确定第二个人与当前的人的年龄差：

```csharp
if (person1.CompareTo(person2) == 0)
{
	Console.WriteLine("Same age.");
}
else if (person1.CompareTo(person2) > 0)
{
	Console.WriteLine("Person 1 is Older.");
}
else
{
	Console.WriteLine("Person 1 is Younger.");
}
```

IComparer也提供了一个方法Compare()，这个方法接受两个对象，返回一个整型结果。这与CompareTo()相同。对于支持IComparer对象，可以使用下面的代码：

```csharp
if (personComparer.Compare(person1, person2) == 0)
{
	Console.WriteLine("Same age.");
}
else if (personComparer.Compare(person1, person2) > 0)
{
	Console.WriteLine("Person 1 is Older.");
}
else
{
	Console.WriteLine("Person 1 is Younger.");
}
```

这两种情况下，提供给方法的参数时System.Object类型。也就是说，可以比较其他任意类型的两个对象。所以在返回结果之前，通常需要进行某种类型比较，如果使用了错误的类型，还会抛出异常。


###### 3）使用IComparable和IComparer接口对集合排序

## 三、转换 ##

#### 1、重载转换运算符

#### 2、as运算符




