本节笔记用来记录C#中对于基于文件系统的数据如何访问操作。

# 文件系统数据 #

本节将介绍如何读写文件。这是许多.NET应用程序的一个基础性工作。

我们要讨论用于创建、读写文件的主要类，支持在C#代码中处理文件系统的类。

文件是在应用程序的实例之间存储数据的一种便利方式，也可以用于在应用程序之间传输数据。文件可以存储用户和应用程序配置，以便在下次运行应用程序时检索它们。定界的文本文件由许多旧系统使用，为了与这些旧系统进行交互，还需要了解如何处理定界数据。

主要内容：

* 流的含义，.NET如何使用流类访问文件
* 如何使用File对象处理文件结构
* 如何读写文件
* 如何在文件中读写格式化的数据
* 如何读写压缩文件
* 如何序列化和反序列化对象
* 如何监控文件和目录的变化

## 一、Stream ##

在.NET中进行所有输入和输出工作都要用到流。流是序列化设备的抽象表示。

序列化设备可以以线性方式存储数据，并可以按同样的方式访问：一次访问一个字节。此设备可以是磁盘文件、网络通道、内存位置或其他支持以线性方式读写的对象。把设备变成抽象的，就可以隐藏流的底层目标和源。

这种抽象的级别支持代码重用，允许编写更通用的例程，因为不必担心数据传输方式的特性。因此，当应用程序从文本输入流、网络输入流或其他流中读取数据时，就可以转换并重用类似的代码。而且，使用文件流还可以忽略每种设备的物理机制，无需担心硬盘头或内存分配问题。

有两种类型的流：

* **输出流：**当向某些外部目标写入数据时，就要用到输出流。这可以是物理磁盘文件、网络位置、打印机或另一个程序。理解流编程技术可以带来许多高级应用。这里只介绍写入磁盘文件。
* **输入流：**用于将数据读入程序可以访问的内存或变量中。到目前位置，我们使用的最常见的输入流形式是键盘。输入流可以来自任何源，在此主要关注读取磁盘文件。

## 二、用于输入和输出的类 ##

System.IO命名空间包含本节几乎所有的类。System.IO包含用于在文件中读写数据的类，必须在C#应用程序中引用此名称空间才能访问这些类，而无需完全限定类型名。在System.IO命名空间中包含了不少类，但这里我们介绍用于文件输入和输出的主要类。

类 | 说明
:--:|:---
File | 静态实用类，提供许多静态方法，用于移动、复制和删除文件
Directory | 静态实用类，提供许多静态方法，用于移动、复制和删除目录
Path | 实用类，用于处理路径名称
FileInfo | 表示磁盘上的物理文件，该类包含处理此文件的方法。要完成对文件的读写工作，就必须创建Stream对象
DirectoryInfo | 表示磁盘上的物理目录，该类包含处理此目录的方法
FileStream | 表示可写或可读，或二者均可的文件。此文件可以同步或异步地读写
FileStreamInfo | 用作FileInfo和DirectoryInfo的基类，可以使用多态性同时处理文件和目录
StreamReader | 从流中读取字符数据，可以使用FileStream将其创建为基类
StreamWriter | 向流中写入字符数据，可以使用FileStream将其创建为基类
FileSystemWatcher | FileSystemWatcher是这里最复杂的类。它用于监控文件和目录，提供了这些文件和目录发生变化时应用程序可以捕获的事件。在Windows编程技术中缺乏此功能，但现在.NET很容器对文件系统事件作出响应

本章还将介绍System.IO.Compression名称空间，它允许使用GZIP压缩或Deflate压缩模式读写压缩文件：

* DeflateStream——表示在写入时自动压缩数据或在读取时自动解压缩的流，使用Deflate算法来实现压缩。
* GZipStream——表示在写入时自动压缩数据或在读取时自动解压缩的流，使用GZIP算法来实现压缩。

最后，学习使用System.Runtime.Serialization名称空间及其子命名空间进行对象的序列化，主要介绍System.Runtime.Serialization.Formatters.Binary名称空间中的BinaryFormatter类，它允许把对象序列化为二进制数据流，并可以反序列化这些数据。

### 1.File类和Directory类

File和Directory实用类提供了许多静态方法，用于处理文件和目录。这些方法可以移动文件、查询和更新特性，创建FileStream对象。可以在类上调用静态方法，而无需创建它们的实例。

File类的一些最常用的静态方法如下：

方法 | 说明
:--:|:---
Copy() | 将文件从源位置复制到目标位置
Create() | 在指定的路径上创建文件
Delete() | 删除文件
Open() | 返回指定路径上的FileStream对象
Move() | 将指定的文件移动到新位置，可以在新位置为文件指定不同的名称

Directory类的一些常用静态方法如下：

方法 | 说明
:--:|:---
CreateDirectory() | 创建具有指定路径的目录
Delete() | 删除指定的目录及其中的所有文件
GetDirectories() | 返回表示指定目录下的目录名的String对象数组
EnumerateDirectories() | 与GetDirectories()类似。但返回目录名的`IEnumerable<string>`集合
GetFiles() | 返回在指定目录中的文件名的string对象数组
EnumerateFiles() | 与GetFiles()类似，但返回文件名的`IEnumerable<string>`集合
GetFileSystemEntries() | 返回在指定目录中的文件和目录名的string对象数组
EnumerateFileSystemEntries() | 与GetFileSystemEntries()类似，但返回文件名的`IEnumerable<string>`集合
Move() | 将指定的目录移到新位置。可以在新位置为文件夹指定一个新名称

注：其中EnumerateXxx()方法是.NET 4新增的，在存在大量文件和目录时，其性能比对应的GetXxx()方法好用。

### 2.FileInfo类

FileInfo类不像File类，它不是静态的，没有静态方法，仅可用于实例化的对象。FileInfo对象表示磁盘或网络位置上的文件。提供文件路径，就可以创建一个FileInfo对象：

```csharp
FileInfo aFile = new FileInfo(@"C:\Log.txt");
```

注意：本章处理的是表示文件路径的字符串，该字符串中有许多“\”字符，所有上述字符串的前缀@表示这个字符串应逐个字符解释，“\”解释为“\”，而不是转义字符。如果没有@前缀，就需要使用“\\”代替“\”，以免把这个字符解释为转义字符，这里总是在字符串前面加上@前缀。

也可以把目录名传送给FileInfo的构造函数，但实际上这并不是很有效。这么做会用所有的目录信息初始化FileInfo的基类FilesSystemInfo，但FileInfo中与文件相关的专用方法或属性都不会工作。

FileInfo类提供的许多方法类似于File类的方法，但是因为File是静态类，它需要一个字符串参数为每个方法调用指定文件位置。因此，下面的调用可以完成相同的工作：

```csharp
FileInfo aFile = new FileInfo("Data.txt");

if (aFile.Exists)
	Console.WriteLine("File Exists");

if (File.Exists("Data.txt"))
	Console.WriteLine("File Exists");
```

这段代码检查文件Data.txt是否存在。注意，这里没有指定任何目录信息，这说明只检查当前的工作目录。这个目录包含调用此代码的应用程序。

大多数FileInfo方法采用这种方式反映File方法。在大多数情况下使用什么技术并不重要，但下面的规则有助于确定那种技术更合适：

* 如果仅进行单一方法调用，则可以使用静态File类上的方法。在此，单一调用要快一些，因为.NET Framework不必实例化新对象，再调用方法。
* 如果应用程序在文件上执行几种操作，则实例化FileInfo对象并使用其方法就更好一些。这节省时间，因为对象已在文件系统上引用正确文件，而静态类必须每次都寻找文件。

FileInfo类也提供了与底层文件相关的属性，其中一些属性可以用来更新文件，其中很多属性都继承于FileSystemInfo，所有可应用于File和Directory类。

FileSystemInfo类的属性如下表：

属性 | 说明
:--:|:---
Attributes | 使用FileAttributes枚举，获得或者设置当前文件或目录的特性
CreationTime,CreationTimeUtc | 获取当前文件的创建日期和时间，可以使用UTC和非UTC版本
Extension | 提取文件的扩展名。这个属性是只读的
Exists | 确定文件是否存在，这是一个只读抽象属性，在FileInfo和DirectoryInfo中进行了重写
FullName | 检索文件的完整路径，这个属性是只读的
LastAccessTime,LastAccessTimeUtc | 获取或设置上次访问当前文件的日期和时间，可以使用UTC和非UTC版本
LastWriteTime,LastWriteTimeUtc | 获取或设置上次写入当前文件的日期和时间，可以使用UTC和非UTC版本
Name | 检索文件的完整路径，这是一个只读抽象属性，在FileInfo和DirectoryInfo中进行了重写

FileInfo的专用属性如下表：

属性 | 说明
:--:|:---
Directory | 检索一个DirectoryInfo对象，表示包含当前文件的目录。这个属性是只读的
DirectoryName | 返回文件目录的路径。这个属性是只读的
IsReadOnly | 文件只读特性的快捷方式。这个属性也可以通过Attributes来访问
Length | 获取文件的容量（以字节为单位），返回long值。这个属性是只读的

FileInfo对象本身不表示流。要读写文件，必须创建Stream对象。FileInfo对象提供了几个返回实例化Stream对象的方法来帮助做到这一点。

### 3.DirectoryInfo类

DirectoryInfo类的作用类似于FileInfo类。它是一个实例化的对象，表示计算机上的单一目录。同FileInfo类一样，在Directory和DirectoryInfo之间可以复制许多方法调用。选择使用File或FileInfo方法的规则也适用于DirectoryInfo方法：

* 如果进行单一调用，就使用静态Directory类。
* 如果进行一系列调用，则使用实例化的DirectoryInfo对象。

DirectoryInfo类的大多数属性继承自FileSystemInfo，与FileInfo类一样，但这些属性作用于目录上，而不是文件上。还有两个DirectoryInfo专用属性，如下表：

属性 | 说明
:--:|:---
Parent | 检索一个DirectoryInfo对象，表示包含当前目录的目录。这个属性是只读的
Root | 检索一个DirectoryInfo对象，表示包含当前目录的根目录，例如C:\目录。这个属性是只读的

### 4.路径名和相对路径

在.NET代码中规定路径名时，可以使用绝对路径名，也可以使用相对路径名。绝对路径名显式地规定文件或目录来自于哪一个已知的位置，比如C:驱动器。它的一个示例是C:\Work\LogFile.txt。注意这个路径准确地定义了其位置。

相对路径名相对于一个起始位置。使用相对路径名时，无需规定驱动器或已知的位置；前面的当前工作目录就是起点，这是相对路径名的默认设置。例如，如果应用程序运行在C:\Development\FileDemo目录上，并使用相对路径LogFile.txt，该文件就是C:\Development\FileDemo\LogFile.txt。为了上移目录，要使用..字符串。这样，在同一个应用程序中，路径..\Log.txt表示C:\Development\Log.txt文件。

如前所述，工作目录起始设置为运行应用程序的目录。当使用VS开发程序时，这就表示应用程序是所创建的项目文件夹下的几个目录。它通常位于Project\bin\Debug。要访问项目根文件夹中的文件，必须用..\..\上移两个目录。

只要需要，就可以使用Directory.GetCurrentDirectory()找出工作目录的当前设置，也可以使用Directory.SetCurrentDirectory()设置新路径。

### 5.FileStream对象

FileStream对象表示在磁盘或网络路径上指向文件的流。这个类提供了在文件中读写字节的方法。但经常使用StreamReader或StreamWriter执行这些功能。这是因为FileStream类操作的是字节和字节数组，而Stream类操作的是字符数据。字符数据易于使用，但是有些操作，如随机文件访问（访问文件中间某点的数据），就必须由FileStream对象执行，稍后对此进行介绍。

还有几种方法可以创建FileStream对象。构造函数具有许多不同的重载版本，最简单的构造函数仅有两个参数，即文件名和FileMode枚举值。

```csharp
FileStream aFile = new FileStream(filename, FileMode.Member);
```

FileMode枚举包含几个成员，指定了如何打开或创建文件。稍后介绍这些枚举成员。另一个常用的构造函数如下：

```csharp
FileStream aFile = new FileStream(filename, FileMode.Member, FileAccess.Member);
```

第三个参数是FileAccess枚举的一个成员，它指定了流的作用。FileAccess枚举的成员如下表：

成员 | 说明
:---:|:----
Read | 打开文件，用于只读
Write | 打开文件，用于只写
ReadWrite | 打开文件，用于读写

对文件进行非FileAccess枚举成员指定的操作会导致抛出异常。此属性的作用是，基于用户的身份验证级别改变用户对文件的访问权限。

在FileStream构造函数不使用FileAccess枚举参数的版本中，使用默认值FileAccess.ReadWrite。

FileMode枚举成员如下表所示。使用每个值会发生什么，取决于指定的文件名是否表示已有的文件。注意，这个表中的项表示创建流时该流指向文件中的位置，除非特别说明，否则流就指向文件的开头。

成员 | 文件存在 | 文件不存在
:---:|:-------|:----------
Append | 打开文件，流指向文件的末尾，只能与枚举FileAccess.Write结合使用 | 创建一个新文件。只能与枚举FileAccess.Write结合使用
Create | 删除该文件，然后创建新文件 | 创建新文件
CreateNew | 抛出异常 | 创建新文件
Open | 打开现有文件，流指向文件开头 | 抛出异常
OpenOrCreate | 打开文件，流指向文件开头 | 创建新文件
Truncate | 打开现有文件，清除其内容。流指向文件开头，保留文件的初始创建日期 | 抛出异常

File和FileInfo类都提供了OpenRead()和OpenWrite()方法，更易于创建FileStream对象。前者打开了只读访问的文件，后者只允许写入文件。这些都提供了快捷方式，因此不必以FileStream构造函数的参数提供前面所有的信息。

例如下面的代码打开了用于只读访问的Data.txt文件：

```csharp
FileStream aFile = File.OpenRead("Data.txt");
```

下面的代码执行同样的功能：

```csharp
FileInfo aFileInfo = new FileInfo("Data.txt");
FileStream aFile = aFileInfo.OpenRead();
```

**1、文件位置**

FileStream类维护内部文件指针，该指针指向文件中进行下一次读写操作的位置。在大多数情况下，当打开文件时，它就指向文件的开始位置，但是可以修改此指针。这允许应用程序在文件的任何位置读写，随机访问文件，或直接跳到文件的特定位置上。当处理大型文件时，这非常省时，因为马上可以找到正确位置。

实现此功能的方法是Seek()方法，它有两个参数：第一个参数规定文件指针移动距离（以字节为单位）。第二个参数规定开始计算的起始位置，用SeekOrigin枚举的一个值表示。SeekOrigin枚举包含3个值：Begin、Current和End。

例如下面的代码行将文件指针移动到文件的第8个字节，其起始位置就是文件的第一个字节：

```csharp
aFile.Seek(8,SeekOrigin.Begin);
```

下面的代码行将指针从当前位置开始向前移动2个字节。如果在上面的代码行之后执行下面的代码，文件指针就指向文件的第十个字节：

```csharp
aFile.Seek(2,SeekOrigin.Current);
```

注意读写文件时，文件指针会随之改变。在读取了10个字节之后，文件指针就指向被读取的第10个字节之后的字节。

也可以规定反向查找位置，这可以与SeekOrigin.End枚举值一起使用，查找靠近文件末端的位置。下面的代码会查找文件中倒数第五个字节：

```csharp
aFile.Seek(-5,SeekOrigin.End);
```

采用这种方式访问的文件有时称为随机访问文件，因为应用程序可以访问文件中的任何位置。稍后介绍Stream类可以连续地访问文件，但不允许以这种方式操作文件指针。

注意：.NET 4引入了一个新名称空间System.IO.MemoryMappedFiles，它包含的类型提供了另一种随机访问特大型文件的方式。这个名称空间在本节不介绍，但如果需要访问特大型文件时，请考虑使用这种方式。

**2、读取数据**

使用FileStream类读取数据不像后面要介绍的StreamReader类读取数据那样容易。这是因为FileStream类只能处理原始字节（raw byte）。处理原始字节的功能使FileStream类可以用于任何数据文件，而不仅仅是文本文件。

通过读取字节数据，FileStream对象可以用于读取诸如图形和声音的文件。这种灵活性的代价是，不能使用FileStream类将数据直接读入字符串，而使用StreamReader类却可以这样处理。但是有几种转换类可以很轻易的将字节数组转换为字符数组，或者将字符数据转换为字节数组。

FileStream.Read()方法是从FileStream对象所指向的文件中访问数据的主要手段。这个方法从文件中读取数据，再把数据写入一个字节数组。

它有三个参数：第一个参数是传入的字节数组，用来接受FileStream对象中的数据。第二个参数是字节数组中开始写入数据的位置：它通常是0，表示从数组开端向文件中写入数据。最后一个参数指定从文件中读出多少字节。

具体实例演示见本节后续代码。

**3、写入数据**

向随机访问文件中写入数据的过程与从中读取数据非常相似。

首先需要创建一个字节数组：最简单的方法是首先构建要写入文件的字符数组，然后使用Encoder对象将其转换为字节数组，其用法非常类似于Decoder对象。最后调用Write()方法，将字节数组传送到文件中。

与Read()方法一样，Write方法也有三个参数：要写入的数组，开始写入的数组索引和要写入的字节数。

### 6.StreamWriter对象

操作字节数组比较麻烦，因为FileStream对象非常困难。因为有了FileStream对象，通常都会把它包装在StreamWriter或StreamReader中，并使用它们的方法来处理文件。如果不需要将文件指针改变到任意位置，这些类就很容易操作文件。

StreamWriter类允许将字符和字符串写入到文件中，它处理底层的转换，向FileStream对象写入数据。

也可以通过许多方法创建StreamWriter对象。如果已经有了FileStream对象，则可以使用此对象来创建StreamWriter对象：

```csharp
FileStream aFile = new FileStream("Log.txt", FileMode.CreateNew);
StreamWriter sw = new StreamWriter(aFile);
```

也可以直接从文件中创建StreamWriter对象：

```csharp
StreamWriter sw = new StreamWriter("Log.txt", true);
```

这个构造函数的参数是文件名和一个Boolean值，这个Boolean值规定是追加文件，还是创建新文件：

* 如果此值设置为false，则创建一个新文件，或者截取现有文件并打开它。
* 如果此值设置为true，则打开文件，保留原来的数据。如果找不到文件，则创建一个新文件。

与创建FileStream对象不同，创建StreamWriter对象不会提供一组类似的选项：除了使用Boolean值追加文件或创建新文件之外，根本没有像FileStream类那样指定FileMode属性的选项。而且，没有设置FileAccess属性的选项，因此总是拥有对文件的读写权限。

为了使用高级参数，必须先在FileStream构造函数中指定这些参数，然后在FileStream对象中创建StreamWriter。实际例子中我们会对这点进行演示。

### 7.StreamReader对象

输入流用于从外部源中读取数据。很多情况下，数据源是磁盘上的文件或网络的某些位置。任何可以发送数据的位置都可以是数据源，比如网络应用程序、Web服务甚至是控制台。

用来从文件中读取数据的类是StreamReader。同StreamWriter一样，这是一个通用类，可以用于任何流。

我们将创建示例，会再次围绕FileStream对象构造StreamReader类，使它指向正确的文件。

StreamReader对象的创建方式和StreamWriter对象非常类似。创建它的最常见方式是使用前面创建的FileStream对象：

```csharp
FileStream aFile = new FileStream("Log.txt", FileMode.Open);
StreamReader sr = new StreamReader(aFile);
```

同StreamWriter一样，StreamReader类可以直接在包含具体文件路径的字符串中创建：

```csharp
StreamReader sr = new StreamReader("Log.txt");
```

**1、读取数据**

ReadLine()方法不是在文件中访问数据的唯一方法。StreamReader类还包含许多读取数据的方法。

读取数据最简单的方法是Read()。此方法将流的下一个字符作为正整数值返回，如果到达了流的结尾处，则返回-1.使用Convert实用类可以把这个值转换为字符。

我们可以像下面这样读取转换文本：

```csharp
StreamReader sr = new StreamReader(aFile);
int nChar;
nChar = sr.Read();
while (nChar != -1)
{
	Console.Write(Convert.ToChar(nChar));
	nChar = sr.Reader();
}
sr.Close();
```

对于小型文件，可以使用一个非常方便的方法ReadToEnd()。此方法读取整个文件，并将其作为字符串返回。在此，上面的程序可以简化为：

```csharp
StreamReader sr = new StreamReader(aFile);
Line = sr.ReadToEnd();
Console.WriteLine(Line);
sr.Close();
```

这似乎比较容易和方便，但必须小心。将所有的数据读取到字符串对象中，会迫使文件中的数据放到内存中。应根据数据文件的大小禁止这样的处理。如果数据文件非常大，最好将数据留在文件中，并使用StreamReader的方法访问文件。

处理大型文件的另一种方式是.NET 4中新增的静态方法File.ReadLines()。实际上，File的几个静态方法可以用于简化文件数据的读写，但这个方法特别有趣，因为它返回`IEnumerable<string>`集合。可以迭代这个集合中的字符串，一次读取文件中的一行。使用这个方法，前面的示例可以重写为：

```csharp
foreach (string alternativeLine in File.ReadLines("Log.txt"))
	Console.WriteLine(alternativeLine);
```

可以看出在.NET中可以通过多种不同的方式获得相同的结果——读取文件中的数据。你可以选择其中最适当的技术。

**2、用分隔符分隔的文件**

用分隔符分隔的文件是一种常见的数据存储形式，由许多旧系统使用，如果应用程序必须与这种操作系统协作，就会经常遇到分隔符分隔的数据格式。最常见的分隔符是逗号，例如Excel电子表格，Access数据库或SQL Server数据库中的数据都可以导出为用逗号分隔的值——CSV文件。

前面介绍了如何使用StreamWriter类编写使用这种方法存储数据的文件。使用逗号分隔的文件也易于读取。String类的Split()方法可以用于将字符串转换为基于所提供的分隔符的数组。如果规定逗号为分隔符，它就会创建尺度合理的字符串数组，其中包含在初始逗号分隔的字符串中的全部数据。

使用.NET Framework从CSV文件中提取有意义的数据非常容易。这种技术也很容易与后面介绍的数据访问技术合并使用，即CSV文件中的数据可以像其他数据源中的数据那样操作。

但从CSV文件中提取的数据没有数据类型信息。当前是把所有的数据看作字符串，但对于企业级的商务应用来说，就需要更进一步，给提取的数据添加类型信息。这可能来自存储在CSV文件中的附加信息，可以手工进行配置，也可以从文件的字符串中推断，具体取决于特定的应用程序。

后面介绍的XML也是一种存储和传输数据的优秀方法，而CSV文件仍然非常普遍，还会使用相当长的时间。逗号分隔文件的优点是非常简洁，因此要比XML文件小些。

### 8.读写压缩文件

在处理文件时，常常会发现文件中有许多空格，耗尽了硬盘空间。图形和声音文件尤其如此。读者可能使用过压缩文件和解压缩文件的工具，当希望带着文件到其他地方去或者把文件邮寄给朋友时，使用这些工具是很方便的。System.IO.Compression名称空间就包含能在代码中压缩文件的类，这些类使用GZIP或Deflate算法，这两种算法都是公开的、免费的，任何人都可以使用的。

但压缩文件并不只是把它们压缩一下就完事了。商业应用程序允许把多个文件放在一个压缩文件中，本节介绍的内容要简单的多：只是把文本数据保存在压缩文件中。不能在外部实用程序中访问这个文件，但这个文件比未压缩版本要小得多。

System.IO.Compression名称空间中有两个压缩流类DeflateStream和GZipStream。它们的工作方式非常类似。对于这两个类，都要用已有的流初始化它们，对于文件，流就是FileStream对象。此后就可以把它们用于StreamReader和StreamWriter了，就像使用其他流一样。除此之外，只需指定流是用于压缩还是解压缩，类就知道要对传送给它的数据执行什么操作。

示例：保存和加载已压缩的文本文件

```csharp
static void SaveComperssedFile(string filename, string data)
{
	FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write);
	GZipStream compressionStream =
	new GZipStream(fileStream, CompressionMode.Compress);
	StreamWriter writer = new StreamWriter(compressionStream);
	writer.Write(data);
	writer.Close();
}
```

代码首先创建一个FileStream对象，然后使用它创建一个GZipStream对象。注意，可以用DeflateStream替换这段代码中的所有GZipStream——这两个类的工作方式相同。使用CompressionMode.Compress枚举值指定数据要进行压缩，然后使用StreamWriter把数据写入文件。

下面再写一个LoadCompressedFile()，它不是保存到文件中，而是把压缩的文件加载到字符串中：

```csharp
static void LoadComperssedFile(string filename)
{
	FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
	GZipStream compressionStream =
	new GZipStream(fileStream, CompressionMode.Decompress);
	StreamReader reader = new StreamReader(compressionStream);
	string data = reader.ReadToEnd();
	reader.Close();
	return data;
}
```

## 三、序列化对象 ##

应用程序常常需要在硬盘上存储数据。本节前面介绍了逐段构建文本和数据文件，但这常常不是最简单的方式。有时最好以对象形式存储数据。

.NET Framework在System.Runtime.Serialization和System.Runtime.Serialization.Formatters名称空间中提供了序列化对象的基础构架，这两个名称空间中的一些类实现了这个基础构架。Framework中有两个可用的实现方式：

* System.Runtime.Serialization.Formatters.Binary：这个名称空间包含了BinaryFormatter类，它能把对象序列化为二进制数据，把二进制数据序列化为对象。
* System.Runtime.Serialization.Formatters.Soap：这个名称空间包含了SoapFormatter类，它能把对象序列化为SOAP格式的XML数据，把SOAP格式的XML数据序列化为对象。

这里只介绍BinaryFormatter，因为还没有学习XML数据。实际上不鼓励使用SoapFormatter格式化器，但希望进行可读的序列化时，仍可以使用它。这两个类都实现了IFormatter接口，这里讨论的很多内容都适用于这两个类。

IFormatter接口提供了下表中所示的方法：

方法 | 说明
:---:|:----
void Serialize(Stream stream, object source) | 把source序列化为stream
object Descrialize(Stream stream) | 反序列化stream中的数据，返回得到的对象

重要的是，为了便于讨论，这些方法都处理流，以便把这些方法与本节前面介绍的文件访问技术联系起来——可以使用FileStream对象。

所有，使用BinaryFormatter进行序列化非常简单：

```csharp
IFormatter serializer = new BinaryFormatter();
serializer.Serialize(myStream, myObject);
```

反序列化同样也很简单：

```csharp
IFormatter serializer = new BinaryFormatter();
myObjectType myNewObject = serializer.Deserialize(myStream) as MyObjectType;
```

显然，需要流和对象才能进行处理，但上面的代码对于几乎所有的情况都是正确的。

序列化涉及许多内容，但前面介绍的基础知识已经足够了。下面要研究一个较高级技术是使用ISerializable接口的定制序列化，它允许定制序列化的数据。例如在发布后升级类时，这是很重要的。修改可序列化的成员，会使所保存的已有数据不再可读，除非提供自己的逻辑来保存和获取数据。

## 四、监控文件系统 ##

有时，应用程序所需要完成的工作不仅仅限于从文件系统中读写文件。例如，知道修改文件或目录的事件非常重要。.NET Framework允许方便地创建完成这些任务的定制应用程序。

帮助完成这些任务的类是FileSystemWatcher。这个类提供了几个应用程序可以捕获的事件。应用程序可以对文件系统事件作出响应。

使用FileSystemWatcher的基本过程非常简单。首先必须设置一些属性，指定监控的位置、内容以及引发应用程序要处理的事件的时间。然后给FileSystemWatcher提供定制事件处理程序的地址，当发生重要事件时，FileSystemWatcher就可以进行调用。最后打开FileSystemWatcher，等待事件。

在启用FileSystemWatcher对象之前必须设置的属性如下表：

属性 | 说明
:---:|:----
Path | 设置要监控的文件位置或目录
NotifyFilter | 这是NotifyFilters枚举值的组合，NotifyFilters枚举值规定在被监控的文件内要监控那些内容。这些表示要监控的文件或文件夹的属性。如果规定的属性发生了变化，就引发事件。可能的枚举值是Attributes、CreationTime、DirectoryName、FileName、LastAccess、LastWrite、Security和Size。注意，可以通过二元OR运算符来合并这些枚举值
Filter | 指定要监控哪些文件的过滤器，例如，*.txt

设置之后，就必须为4个事件Changed、Created、Deleted和Renamed编写事件处理程序。这里需要创建自己的方法，并将方法赋给对象的事件。将自己的事件处理程序赋给这些方法，就可以在引发事件时调用方法。当修改与Path、NotifyFilter和Filter属性匹配的文件或目录时，就引发每个事件。

在设置了属性和事件后，将EnabledRaisingEvents属性设置为true，就可以开始监控工作。

创建私有方法为监控器对象挂接事件处理程序的方式。当删除、重命名、修改或创建文件时，监控器对象就触发事件，调用事件处理程序。用户在自己的方法中可以决定如何处理实际发生的事件。注意，在事件发生之后我们才会得到通知。

在实际的事件处理程序中，只是将事件写入日志文件。显然，根据应用程序的不同，还可以有更复杂的响应。在目录中添加文件时，可以将其移动到别处，或读取其内容，引发新的进程。其可能的用法是无穷尽的！