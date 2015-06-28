本篇笔记简单记录下调试与错误处理这一章的重点条目。

## 调试和错误处理 ##

代码难免存在错误。我们遇到简单的拼写错误或语法错误，通过排错，很容易得到修正。遇到导致程序完全失败的致命错误，也许你需要重新考虑处理问题的方法。而应用程序逻辑有瑕疵时，就会产生语义错误或逻辑错误。

在IDE中我们可以通过一些工具帮助我们处理错误。

#### 7.1 在VS和VCE中调试

我们前面见到最常见的输出信息函数是Console.WriteLine()函数，它将文本输出到控制台上。但这样其实信息是比较混乱的。若是在Windows窗体程序中，没有控制台，该怎么办？我们一般把文本信息输出到IDE的Output窗口。这个窗口下有Build和Debug两种模式，我们一般进入Debug模式视图。

当然，还可以创建日志文件，运行程序时将信息添加到该日志文件中。

**1、输出调试信息**

将Console.WriteLine()替换就可以把文本输出到希望的地方。可以使用如下两个命令：

- Debug.WriteLine()
- Trace.WriteLine()

这两个命令基本一致，只有一个区别，第一个命令仅在调试模式下运行，第二个命令还可用于发布程序。与Console.WriteLine()不同，这两种函数一般输出如下所示：

	<category>: <message>

eg: 下面语句MyFunc作为可选类别参数：

	Debug.WriteLine("Added 1 to i", "MyFunc");

输出结果：

	MyFunc: Add 1 to i

这组函数还包括`Debug.Write()`、`Trace.Write()`、`Debug.WriteLineIf()`、`Trace.WriteLineIf()`、`Debug.WriteIf()`、`Trace.WriteIf()`.
这些函数参数都与没有if的对应函数相同，但增加了一个必选参数，且该参数放在列表参数的最前面。这个参数的值为布尔型，只有这个值为true时，函数才会输出文本。

**2、使用跟踪点**

跟踪点是VS中特有的功能，不是C#的。所以跟踪点未包含在程序中，与trace不等价。

总之，**输出调试信息的两种方法是：**

- 诊断输出：总是要从程序中输出调试结果时使用这种方法，尤其时输出字符串比较复杂时，使用该方法比较好；
- 跟踪点：调试时，想要快速输出重要信息，以便快速解决语义错误，使用跟踪点。

IDE中还有断点模式、watch窗口、local窗口、单步执行模式、Call Stack窗口、Immediate窗口和Command窗口。

#### 7.2 错误处理

我们不能避免错误百分百不会发生。异常在命名空间中定义，大多数异常的名称清晰地说明了它们的用途。如`System.IndexOutOfRangeException`，说明我们提供的myArray数组索引不在允许使用的索引范围内。

**1、try...catch...finally**

C#包含三个关键字标记能处理异常的代码和指令。其基本结构如下：

	try
	{
		...
	}
	catch
	{
		...
	}
	finally
	{
		...
	}

try是必选的，catch可以有0个到多个，finally可选，只能有一个。用法如下：

- **try**——包含抛出异常的代码；
- **catch**——包含抛出异常时要执行的代码。catch块可以使用`<exceptionType>`，设置为只响应特定异常类型，以便提供多个catch块。还可以完全忽略这个参数，让一般catch块响应所有异常。
- **finally**——包含总是会执行的代码，如果没有产生异常，则在try块之后执行，如果处理了异常，就在catch块后执行，或在未处理的异常上移到调用堆栈前执行。

在try块的代码中出现异常后，发生的事件依次是：

- try块在发生异常的地方中断程序的执行。
- 如果有catch块，就检查该块是否匹配已抛出的异常类型。如果没有catch块，就执行finally块（没有catch块时，就一定要有finally块）。
- 如果有catch块，但与已发生的异常类型不匹配，就检查是否有其他catch块。
- 如果有catch块匹配已发生的异常类型，就执行它包含的代码，再执行finally块（如果有）。
- 如果catch块都不匹配已发生的异常类型，就执行finally块（如果有）。

**2、列出和配置异常**

IDE中会将异常展示在Exception对话框内。

**3、异常处理的注意事项**

注意，必须在更一般的异常捕获前为比较特殊的异常提供catch块。如果catch块顺序错误，应用程序就会编译失败。可以在catch中抛出异常，需要使用下面的表达式：

	throw；