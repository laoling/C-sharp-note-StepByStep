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

#### 6、深复制

## 二、比较 ##

#### 1、类型比较

###### 1）封箱和拆箱

###### 2）is运算符

#### 2、值比较

###### 1）运算符重载

###### 2）IComparable和IComparer接口

###### 3）使用IComparable和IComparer接口对集合排序

## 三、转换 ##

#### 1、重载转换运算符

#### 2、as运算符




