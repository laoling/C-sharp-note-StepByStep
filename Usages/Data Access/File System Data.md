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

### 6.StreamWriter对象

### 7.StreamReader对象

### 8.读写压缩文件

## 三、序列化对象 ##

## 四、监控文件系统 ##