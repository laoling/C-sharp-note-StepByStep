我们将上节笔记中第二部分和第三部分进行分拆，分别写入新的笔记文档中。这节笔记我们先看看XAML。

# 二、XAML #

XAML是Extensible Application Markup Language的简写，发音是zammel。是用于实例化.NET对象的标记语言。

尽管XAML是一种可应用于诸多不同问题领域的技术，但其主要作用是构造WPF用户界面。换言之，XAML文档定义了在WPF应用程序中组成窗口的面板、按钮以及各种控件的布局。

不必再动手编写XAML，您将使用工具生成所需的XAML。如果您是一位图形设计人员，该工具可能是图形设计程序，如Expression Blend。如果您是一名开发人员，您开始时可能使用Microsoft Visual Studio。这两个工具在生成XAML时本质上是相同的，因此可使用Visual Studio创建一个基本用户界面，然后将该界面移交给一个出色的设计团队，由设计团队在Expression Blend中使用自定义图形润色这个界面。实际上，将开发人员和设计人员的工作流程集成起来的能力，是Microsoft推出XAML的重要原因之一。

这里我们将详细介绍XAML，分析XAML的作用、宏观体系结构以及语法。一旦理解了XAML的一般性规则，就可以了解在WPF用户界面中什么是可能的、什么是不可能的，并了解在必要时如何手动修改用户界面。更重要的是，通过分析WPF XAML文档中的标签，可学习一些支持WPF用户界面的对象模型，从而为进一步深入分析WPF用户界面做好准备。

## 1、理解XAML ##

开发人员很久前就已经意识到，要处理图形丰富的复杂应用程序，最有效的方式是将图形部分从底层的代码中分离出来。这样一来，美工人员可独立地设计图形，而开发人员可独立地编写代码。这两部分工作可单独地进行设计和修改，而不会有任何版本问题。

#### 1.1 WPF之前的图形用户界面

使用传统的显示技术，从代码中分离出图形内容并不容易。对于窗体应用程序而言，关键是创建的每个窗体完全都是由C#代码定义的。在将控件拖动到设计视图上并配置控件时，Visual Studio将在相应的窗体类中自动调整代码。但图形设计人员没有任何可以使用C#代码的工具。

相反，设计人员必须将他们工作内容导出为位图。然后可使用这些位图确定窗体、按钮及其他控件的外观。对于简单的固定用户界面而言，这种方法效果不错，但在其他一些情况下会受到很大的限制。这种方法存在以下几个问题：

* 每个图形元素需要导出为单独的位图。这限制了组合位图的能力和使用动态效果的能力，如反锯齿、透明和阴影效果。
* 相当多的用户界面逻辑都需要开发人员嵌入到代码中，包括按钮的大小、位置、鼠标悬停效果及动画。图形设计人员无法控制其中的任何细节。
* 在不同的图形元素之间没有固有的连接，所以最后经常会使用不匹配的图像集合。跟踪所有这些项会增加复杂性。
* 在调整图形大小时必然会损失质量。因此，一个基于位图的用户界面是依赖于分辨率的。这意味着它不能适应大显示器以及高分辨率显示设置，而这严重背离了WPF的设计初衷。

如果曾经有过在一个团队中使用自定义图形来设计Windows窗体应用程序的经历，肯定遇到过不少挫折。即使用户界面是由图形设计人员从头开始设计的，也需要使用C#代码重新创建它。通常，图形设计人员只是准备一个模拟界面，然后需要开发人员再不辞辛苦的将它转换到应用程序中。

WPF通过XAML解决了该问题。当在Visual Studio中设计WPF应用程序时，当前设计的窗口不被转换为代码。相反，它被串行化到一系列XAML标签中。当运行应用程序时，这些标签用于生成构成用户界面的对象。

也就是说，WPF不见得一定要使用XAML。但XAML为协作提供了可能，因为其他设计工具理解XAML格式。

#### 1.2 XAML变体

实际上术语XAML有多种含义。到目前为止，我们使用XAML表示整个XAML语言，它是一种基于通用XML语法、专门用于表示一棵.NET对象树的语言。（这些语言对象可以是窗口中的按钮、文本框、或是您已经定义好的自定义类。实际上，XAML甚至可用于其他平台来表示非.NET对象。）

XAML还包括如下几个子集：

* WPF XAML包含描述WPF内容的元素，如矢量图形、控件以及文档。目前，它是最重要的XAML应用，也是本书将要分析的一个子集。
* XPS XAML是WPF XAML的一部分，它为格式化的电子文档定义了一种XML表示方式。XPS XAML已作为单独的XML页面规范（XML Paper Specification）标准发布。
* Silverlight XAML 是一个用于Silverlight应用程序的WPF XAML子集。Silverlight是一个跨平台的浏览器插件，通过它可创建具有二维图形、动画、音频和视频的富Web内容。
* WF XAML包括描述WF（Workflow）内容的元素。

#### 1.3 XAML编译

WPF的创建者知道，XAML不仅要能够解决设计协作问题，它还需要快速运行。尽管基于XML的格式可以很灵活并且很容易地迁移到其他工具和平台，但它们未必是最有效的选择，XML的设计目标是具有逻辑性、易读而且简单，没有被压缩。

WPF使用BAML（Binary Application Markup Language二进制应用程序标记语言）来克服这个缺点。BAML并非新事物，它实际上就是XAML的二进制表示。当在Visual Studio中编译WPF应用程序时，所有XAML文件都被转换为BAML，这些BAML然后作为资源被嵌入到最终的DLL或EXE程序集中。BAML是标记化的，这意味着较长的XAML被较短的标记替代。BAML不仅明显小一些，还对其进行了优化，从而使它在运行时能够更快的解析。

大多数开发人员不必考虑XAML向BAML的转换，因为编译器会在后台执行这项工作。但也可以使用未经编译的XAML，这对于需要即时提供一些用户界面的情况可能是有意义的。

## 2、XAML基础 ##

一旦理解了一些基本规则，XAML标准是非常简单的：

* XAML文档中的每个元素都映射为.NET类的一个实例。元素的名称也完全对应于类名。例如，元素`<Button>`指示WPF创建Button对象。
* 与所有XML文档一样，可在一个元素中嵌套另一个元素。您在后面将看到，XAML让每个类灵活地决定如何处理嵌套。但嵌套通常是一种表示包含的方法——换句话说，如果在一个Grid元素中发现一个Button元素，那么用户界面可能包括一个在其内部包含一个Button元素的Grid元素。
* 可通过特性（attribute）设置每个类的属性（Property）。但在某些情况下，特性不足以完成这项工作。对于这类情况，需要通过特殊的语法使用嵌套的标签（tag）。

一个新的空白窗口：

```xml
<window x:Class="WindowsApplication1.Window1"
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	Title="Window1" Height="300" Width="300">
	<Grid>

	</Grid>
</window>
```

该文档仅包含两个元素——顶级的Window元素以及一个Grid元素，Window元素代表整个窗口，在Grid元素中可以放置所有控件。尽管可使用任何顶级元素，但是WPF应用程序只使用以下几个元素作为顶级元素：

* Window元素
* Page元素（与Window元素类似，但它用于可导航的应用程序）
* Application元素（该元素定义应用程序资源和启动设置）

与在所有XML文档中一样，XAML文档中只能用一个顶级元素。上面的代码中，意味着只要使用`</window>`标签关闭了Window元素，文档就结束了。在后面就不能再有任何内容了。

查看Window元素的开始标签，将发现几个有趣的特性，包括一个类名和两个XML名称空间。还会发现三个属性，如下所示：

	Title="Window1" Height="300" Width="300"

每个特性对应Window类的一个单独属性。总之，这告诉我们WPF创建标题为Window1的窗口，并使窗口的大小为300X300单位。

#### 2.1 XAML名称空间

显然，只提供类名是不够的。XAML解析器还需要知道类位于哪个.NET名称空间。例如，在许多名称空间中可能都有Window类——Window类可能是指System.Windows.Window类，也可能是指位于第三方组件中的Window类，或您自己在应用程序中定义的Window类等。为了弄清楚实际上希望使用哪个类，XAML解析器会检查应用于元素的XML名称空间。

下面是该机制的工作原理。上面显示的示例文档定义了两个名称空间：

	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"

xmlns特性是XML中一个特殊特性，它专门用来声明名称空间。这段标记声明了两个名称空间，在创建的所有WPF XAML文档中都会使用这两个名称空间：

* `http://schemas.microsoft.com/winfx/2006/xaml/presentation`是WPF核心名称空间。它包含了所有的WPF类，包括用来构建用户界面的控件。在该例中，该名称空间的声明没有使用名称空间前缀，所以它成为整个文档的默认名称空间。换句话说，除非另行指明，每个元素自动位于这个名称空间。
* `http://schemas.microsoft.com/winfx/2006/xaml`是XAML名称空间。它包含各种XAML实用特性，这些特性可影响文档的解释方式。该名称空间被映射为前缀x。这意味着可通过在元素名称前放置名称空间前缀x来使用该名称空间，例如`<x:ElementName>`。

正如在前面看到的，XML名称空间的名称和任何特定的.NET名称空间都不匹配。XAML的创建者选择这种设计的原因有两个。按照约定，XML名称空间通常是URI。这些URI看起来像是在指明Web上的位置，但实际上不是。通过使用URI格式的名称空间，不同组织就基本不会无意中使用相同的名称空间创建不同的基于XML的语言。因为schemas.com域名归于Microsoft，只有Microsoft会在XML名称空间的名称中使用它。

另一个原因是XAML中使用的XML名称空间和.NET名称空间不是一一对应的，如果一一对应的话，会显著增加XAML文档的复杂度。此处的问题在于，WPF包含了十几种名称空间，所有这些名称空间都以System.Windows开头。如果每个.NET名称空间都有不同的XML名称空间，那就需要为使用的每个控件指定确切的XML名称空间，这很快就会使XAML文档变得混乱不堪。所以，WPF创建人员选择了这种方法，将所有这些.NET名称空间组合到单个XML名称空间中。因为在不同的.NET名称空间中都有一部分WPF类，并且所有这些类的名称都不相同，所以这种设计是可行的。

名称空间信息使得XAML解析器可找到正确的类。例如，当查找Window和Grid元素时，首先会查找默认情况下它们所在的WPF名称空间，然后查找相应的.NET名称空间，直至找到System.Windows.Window类和System.Windows.Controls.Grid类。

#### 2.2 XAML代码隐藏类

可通过XAML构造用户界面，但为了使应用程序具有一定的功能，就需要用于连接包含应用程序代码的事件处理程序的方法。XAML通过使用如下所示的Class特性简化了这个问题：

```xml
<Window x:Class="WindowsApplication1.Window1"
```

在XAML名称空间的Class特性之前放置了名称空间前缀x，这意味着这是XAML语言中更通用的部分。实际上，Class特性告诉XAML解析器用指定的名称生成一个新类。该类继承自由XML元素命名的类。换句话说，该例创建了一个名为Window1的新类，该类继承自Window基类。

Window1类是编译时自动生成的，您可以提供Window1的部分类，该部分类会与自动生成的那部分合并在一起。您提供的部分类正是包含事件处理程序代码的理想容器。

Visual Studio会自动帮助您创建可以放置事件处理代码的部分类。例如如果创建一个名为WindowsApplication1的应用程序，该应用程序包含名为Window1的窗口，Visual Studio将首先提供基本的类框架：

```csharp
namespace WindowsApplication1
{
	///<summary>
	///Interaction logic for Window1.xaml
	///</summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
		}
	}
}
```

在编译应用程序时，定义用户界面的XAML被转换为CLR类型声明，这些类型声明与代码隐藏类文件（如Window1.xaml.cs）中的逻辑代码融合到一起，形成单一的单元。

**1、InitializeComponent()方法**

现在，Window1类尚不具有任何真正的功能。然而，它确实包含了一个非常重要的细节——默认构造函数，当创建类的一个实例时，该构造函数调用InitializeComponent()方法。

注意：InitializeComponent()方法在WPF中扮演着重要角色。因此，永远不要删除窗口构造函数中的InitializeComponent()调用。同样，如果为窗口类添加另一个构造函数，也要确保调用InitializeComponent()方法。

InitializeComponent()方法在源代码中不可见，因为它是在编译应用程序时自动生成的。本质上，InitializeComponent()方法的所有工作就是调用System.Windows.Application类的LoadComponent()方法。LoadComponent()方法从程序集中提取BAML，并用它来构建用户界面。当解析BAML时，它会创建每个控件对象，设置其属性，并关联所有事件处理程序。

**2、命名元素**

还有一个需要考虑的细节。在代码隐藏类中，经常希望通过代码来操作控件。例如，可能需要读取或修改属性，或自由地关联以及断开事件处理程序。为达到此目的，控件必须包含XAML Name特性。在上面的示例中。Grid控件没有包含Name特性，所有不能在代码隐藏文件对其进行操作。

下面的标记演示了如何为Grid控件关联名称：

```xml
<Grid x:Name="grid1">
</Grid>
```

可在XAML文档中手动执行这个修改，也可以在Visual Studio设计器中选择该网络，并通过属性窗口设置其Name属性。

无论使用哪种方法，Name特性都会告诉XAML解析器将这样一个字段添加到为Window1类自动生成的部分：

```csharp
private System.Windows.Controls.Grid grid1;
```

该技术没有为这个简单的网格添加更多内容，但当需要从输入控件读取数值时它将变得更重要。

上面显示的Name属性是XAML语言的一部分，用于帮助集成代码隐藏类。

## 3、XAML中的属性和事件 ##

到目前为止，我们只考虑了一个比较单调的示例——包含一个空Grid控件的空白窗口。在继续学习之前，有必要首先介绍一个更贴近实际的包含几个控件的窗口。这个窗口包含4个控件，一个Grid控件，两个TextBox控件和一个Button控件。下面我们列出来这些标记，细节使用...代替，以便于整体描述：

```xml
<Window x:class="EightBall.Window1"
	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
	Title="Eight Ball Answer" Height="328" Width="412">
  <Grid Name="grid1">
	<Grid.Background>
	  ...
	</Grid.Background>
	<Grid.RowDefinitions>
	  ...
	</Grid.RowDefinitions>
	<TextBox Name="txtQuestion" ...>
	  ...
	</TextBox>
	<Button Name="cmdAnswer" ...>
	  ...
	</Button>
	<TextBox Name="txtAnswer" ...>
	  ...
	</TextBox>
  </Grid>
</Window>
```

下面我们分析该文档的各个部分，并学习XAML的语法。

#### 3.1 简单属性与类型转换器

前面已经介绍了，元素的特性设置相应对象的属性。例如，我们为上面示例中的文本框设置对齐方式、页边距和字体：

```xml
<TextBox Name="txtQuestion"
	VerticalAlignment="Stretch" HorizontalAlignment="Stretch"
	FontFamily="Verdana" FontSize="24" Foreground="Green" ...>
```

为使上面的设置起作用，System.Windows.Controls.TextBox类必须提供以下属性：VerticalAlignment、HorizonAlignment、FontFamily、FontSize和Foreground。后面几章将介绍这些属性的具体含义。

为使这个系统能够工作，XAML解析器需要执行比表面上看起来更多的工作。XML特性中的值总是纯文本字符串。但对象的属性可以是任何.NET类型。在上面的示例中，有两个属性为枚举类型（VerticalAlignment属性和HorizonAlignment属性）、一个为字符串类型（FontFamily属性）、一个整型（FontSize属性），还有一个为Brush对象（Foreground属性）。

为了关联字符串值和非字符串属性，XAML解析器需要执行转换。由类型转换器执行转换，类型转换器是从.NET1.0起就已经引入的.NET基础结构的一个基本组成部分。

实际上，类型转换器在这个过程中扮演着重要角色——提供了实用的方法，这些方法可将特定的.NET数据类型转换为任何其他.NET类型，或将其他任何类型转换为特定的数据类型，比如这种情况下的字符串类型。XAML解析器通过以下两个步骤来查找类型转换器：

1）检查属性声明，查找TypeConverter特性（如果提供TypeConverter特性，该特性将指定哪个类可执行转换）。

2）如果在属性声明中没有TypeConverter特性，XAML解析器将检查对应数据类型的类声明。

如果属性声明或类声明都没有与其相关联的类型转换器，XAML解析器会生成错误。

这个系统简单灵活。如果在类层次上设置一个类型转换器，该转换器将应用到所有使用这个类的属性上。另一方面，如果希望为某个特定属性微调类型转换方式，那么可以在属性声明中改用TypeConverter特性。

#### 3.2 复杂属性

虽然类型转换器便于使用，但它们不能解决所有的实际问题。例如，有些属性是完备的对象，这些对象具有自己的一组属性。尽管创建供类型转换器使用的字符串表示形式是可能的，但使用这种方式时语法可能十分复杂，并且容易出错。

幸运的是，XAML提供了另一种选择：属性元素语法。使用属性元素语法，可添加名称形式为Parent.PropertyName的子元素。例如Grid控件有一个Background属性，该属性允许提供用于绘制控件北京区域的画刷。如果希望使用更复杂的画刷——比单一固定颜色填充更高级的画刷——就需要添加名为Grid.Background的子标签。如下：

```xml
<Grid Name="grid1">
  <Grid.Background>
	...
  </Grid.Background>
	...
</Grid>
```

真正起作用的重要细节是元素名中的句点。这个句点把该属性和其他类型的嵌套内容区分开来。

还有一个细节，即一旦识别出想要配置的复杂属性，该如何设置呢？

这里有一个技巧：可在嵌套元素内部添加其他标签来实例化特定的类。在上面例子中，用渐变颜色填充背景。为了定义所需的渐变颜色，需要创建LinearGradientBrush对象。

根据XAML规则，可使用名为LinearGradientBrush的元素创建LinearGradientBrush对象：

```xml
<Grid Name="grad1">
  <Grid.Background>
	<LinearGradientBrush>
	<LinearGradientBrush>
  </Grid.Background>
  ...
</Grid>
```

LinearGradientBrush类是WPF名称空间集合中的一部分，所以可为标签继续使用默认的XML名称空间。

但是，只是创建LinearGradientBrush对象还不够——还需要为其指定渐变的颜色。通过使用GradientStop对象的集合填充LinearGradientBrush.GradientStops属性可完成这一任务。同样的，由于GradientStops属性太复杂，因此不能通过一个简单的特性值设置该属性。需要该用属性元素语法：

```xml
<Grid Name="grad1">
  <Grid.Background>
	<LinearGradientBrush>
	  <LinearGradientBrush.GradientStops>
	  </LinearGradientBrush.GradientStops>
	<LinearGradientBrush>
  </Grid.Background>
  ...
</Grid>
```

最后，可以使用一系列GradientStop对象填充GradientStops集合。每个GradientStop对象都有Offset和Color属性。可使用普通的属性——特性语法提供这两个值：

```xml
<Grid Name="grad1">
  <Grid.Background>
	<LinearGradientBrush>
	  <LinearGradientBrush.GradientStops>
		<GradientStop Offset="0.00" Color="Red" />
		<GradientStop Offset="0.50" Color="Indigo" />
		<GradientStop Offset="1.00" Color="Violet" />
	  </LinearGradientBrush.GradientStops>
	<LinearGradientBrush>
  </Grid.Background>
  ...
</Grid>
```

任何XAML标签集合都可以用一系列执行相同任务的代码语句代替。上面显示的使用所选的渐变填充背景的标签，与以下代码是等价的：

```csharp
LinearGradientBrush brush = new LinearGradientBrush();

GradientStop gradientStop1 = new GradientStop();
gradientStop1.Offset = 0;
gradientStop1.Color = Colors.Red;
brush.GradientStops.Add(gradientStop1);

GradientStop gradientStop2 = new GradientStop();
gradientStop2.Offset = 0.5;
gradientStop2.Color = Colors.Indigo;
brush.GradientStops.Add(gradientStop2);

GradientStop gradientStop3 = new GradientStop();
gradientStop3.Offset = 1;
gradientStop3.Color = Colors.Violet;
brush.GradientStops.Add(gradientStop3);
```

#### 3.3 标记扩展

对大多数属性而言，XAML属性语法可以工作得非常好。但有些情况下，不可能硬编码属性值。例如希望将属性值设置为一个已经存在的对象，或者可能希望通过将一个属性绑定到另一个控件来动态地设置属性值。

这两种情况都需要使用标记扩展——一种非常规的方式设置属性的专门语法。

标记扩展可用于嵌套标签或XAML特性中（用于XML特性的情况更常见）。当用在特性中时，它们总是被花括号{}包围起来。例如：

下面的标记演示了如何使用标记扩展，它允许引用另一个类中的静态属性

```xml
<Button ... Foreground="{x:Static SystemColors.ActiveCaptionBrush}" >
```

标记扩展使用{标记扩展类 参数}语法。在上面的示例中，标记扩展是StaticExtension类，根据约定，在引用扩展类时可以省略最后一个单词Extension。x前缀指示在XAML名称空间中查找StaticExtension类。还有一些标记扩展是WPF名称空间的一部分，它们不需要x前缀。

所有的标记扩展都由继承自System.Windows.Markup.MarkupExtension基类的类实现。MarkupExtension基类十分简单——它提供了一个简单的Provide Value()方法来获取所期望的数值。

换句话说，当XAML解析器遇到上述语句时，它将创建StaticExtension类的一个实例（传递字符串SystemColors.ActiveCaptionBrush作为构造函数的参数），然后调用ProvideValue()方法获取SystemColors.ActiveCaptionBrush静态属性返回的对象。最后使用检索的对象设置cmdAnswer按钮的Foreground属性。

这段XAML的最终结果与下面的相同：

```csharp
cmdAnswer.Foreground = SystemColors.ActiveCaptionBrush;
```

因为扩展标记映射为类，所以它们也可用作嵌套属性。例如可以像下面这样为Button.Foreground属性使用StaticExtension标记扩展：

```xml
<Button ...>
  <Button.Foreground>
	<x:Static Member="SystemColors.ActiveCaptionBrush"></x:Static>
  </Button.Foreground>
</Button>
```

根据标记扩展的复杂程度，以及想要设置的属性数量，这种语法有时更简单。

和大多数标记扩展一样，StaticExtension需要在运行时赋值，因为只有在运行时才能确定当前的系统颜色。一些标记扩展可在编译时评估。这些扩展包括NullExtension（该扩展构造表示.NET类型的对象）。

#### 3.4 附加属性

除了普通属性外，XAML还包括附加属性的概念——附加属性是可用于多个控件但在另一个类中定义的属性。在WPF中，附加属性常用于控件布局。

下面解释附加属性的工作原理。每个控件都有各自固定的属性。当在容器中放置控件时，根据容器的类型控件会获得额外特征。使用附加属性设置这些附加的细节。

附加属性始终使用包含两个部分的命名形式：定义类型.属性名。这种包含两部分的命名语法使XAML解析器能够区分开普通属性和附加属性。

在上面示例中，我们通过附加属性在网格的每一行中放置各个控件：

```xml
<TextBox ... Grid.Row="0">
  [Place question here.]
</TextBox>

<Button ... Grid.Row="1">
  Ask the Eight Ball
</Button>

<TextBox ... Grid.Row="2">
  [Answer will appear here.]
</TextBox>
```

附加属性根本不是真正的属性。它们实际上被转换为方法调用。XAML解析器采用以下形式调用静态方法：*DefiningType.SetPropertyName()*。在上面的XAML中，定义类型是Grid类，并且属性是Row，所以解析器调用Grid.SetRow()方法。

当调用SetPropertyName()方法时，解析器传递两个参数：被修改的对象以及指定的属性值。例如当为TextBox控件设置Grid.Row属性时，XAML解析器执行下面的代码：

```csharp
Grid.SetRow(txtQuestion, 0);
```

这种方式隐藏了实际发生的操作，使用起来非常方便。乍一看，这些代码好像将行号保存在Grid对象中。但行号实际上保存在应用它的对象中——对于上面的示例，就是TextBox对象。

这种技巧之所以能够奏效，是因为与其他所以的WPF控件一样，TextBox控件继承自DependencyObject基类。

实际上，GridSetRow()方法是和DependencyObject.SetValue()方法调用等价的简化操作。如下所示：

```csharp
txtQuestion.SetValue(Grid.Rowproperty, 0);
```

附加属性是WPF的核心要素。他们充当通用的可扩展系统。另一个选择是将该属性作为基类的一部分，但这样做很复杂。因为只有在特定情况下，有些属性才有意义，如果它们作为基类的一部分，不仅会使公共接口变得十分杂乱，而且也不能添加需要新属性的容器。

#### 3.5 嵌套元素

正如您所看到的，XAML文档被排列成一颗巨大的嵌套的元素树。在当前示例中，Window元素包含Grid元素，Grid元素又包含TextBox元素和Button元素。

XAML让每个元素决定如何处理嵌套元素。这种交互使用下面三种机制中的一种进行中转，而且求值的顺序也是下面列出这三种机制的顺序：

* 如果父元素实现了IList接口，解析器将调用IList.Add()方法，并且为该方法传入子元素作为参数。
* 如果父元素实现了IDictionary接口，解析器将调用IDictionary.Add()方法，并且为该方法传递子元素作为参数。当使用字典集合时，还必须设置x:Key特性以便为每个条目指定键名。
* 如果父元素使用ContentProperty特性进行修饰，解析器将使用子元素设置对应的属性。

例如，您已经在前面的示例中看到LinearGradientBrush画刷如何使用下面所示的语法，从而包含GradientStop对象集合：

```xml
<LinearGradientBrush>
  <LinearGradientBrush.GradientStops>
	<GradientStop Offset="0.00" Color="Red" />
	<GradientStop Offset="0.50" Color="Indigo" />
	<GradientStop Offset="1.00" Color="Violet" />
  </LinearGradientBrush.GradientStops>
<LinearGradientBrush>
```

因为包含一个句点，所以XAML解析器知道LinearGradientBrush.GradientStops是复杂属性。但它需要以稍有不同的方式处理内部的标签（即三个GradientStop元素）。在这个示例中，解析器知道GradientStops属性返回一个GradientStopCollection类实现了IList接口。因此，解析器假定（也正是如此）应当使用IList.Add()方法将每个GradientStop对象添加到集合中：

```csharp
GradientStop gradientStop1 = new GradientStop();
gradientStop1.Offset = 0;
gradientStop1.Color = Colors.Red;
IList list = brush.GradientStops;
list.Add(gradientStop1);
```

有些属性可支持多种类型的集合。在这种情况下，需要添加一个标签来指定集合类，如下所示：

```xml
<LinearGradientBrush>
  <LinearGradientBrush.GradientStops>
	<GradientStopCollection>
	  <GradientStop Offset="0.00" Color="Red" />
	  <GradientStop Offset="0.50" Color="Indigo" />
	  <GradientStop Offset="1.00" Color="Violet" />
	</GradientStopCollection>
  </LinearGradientBrush.GradientStops>
<LinearGradientBrush>
```

嵌套的内容并非总是指定为集合。前面有个主体Grid元素。其中各个标签都没有包含句点，因此并未对应复杂属性。而且，Grid控件也不是集合，所以它也就没有实现IList或IDictionary接口。Grid控件支持ContentProperty特性，该特性指出应当接收任意嵌套内容的属性。从技术角度看，ContentProperty特性被应用于Panel类，而Grid类继承自Panel类。

XAML解析器根据是否是集合属性采用不同方式处理内容属性。

WPF中经常使用ContentProperty特性。该特性不仅用于容器控件和那些包含可视化条目集合的控件，也用于包含单一内容的控件。

#### 3.6 特殊字符与空白

XAML受到XML规则的限制。例如，XML特别关注一些特殊字符，如&、<和>。如果试图使用这些字符设置元素的内容，将会遇到麻烦，因为XAML解析器认为您正在处理其他事情——例如创建嵌套的元素。

例如在`<Click me>`中，解析器会认为你正在试图创建一个名为Click，并且带有文本的元素。解决的方法是用实体引用代替那些特殊字符。常用字符实体我这里就不再写了。

注意，只有当使用特性设置属性值时，才需要使用引号字符实体，因为引号用于指示特性值的开始和结束。

上面的例子我们可以写成这样：`&lt;Click me&gt;`

当XAML解析器遇到这些标记时，它能正确地理解到您希望添加的文本，而且解析器为属性传递具有相应内容的字符串，字符串内容将包含完整的尖括号。

特殊字符并非是使用XAML的唯一障碍。另一个问题是空白的处理。默认情况下，XAML折叠所有空白，这意味着包含空格、Tab键以及硬回车的长字符串将被转换为单个空格。而且，如果在元素内容之前或之后添加空白，将完全忽略这个空格。

我们有时使用硬回车分离字符，用Tab字符使标记更加清晰易读。但有时这并不是所期望的结果。

比如有时希望在按钮文本中包含一系列空格。在这种情况下，需要为元素使用xml:space="preserve"特性。

xml:space特性是XML标准的一部分，是一个要么包含全部、要么什么都不包括的设置。一旦使用了该设置，元素内的所有空白字符都将被保留。

如果只想保留内部的空格，那么需要使用不清晰的标记。这样的技巧，确保在开始符号>和具体内容之间，以及具体内容和结束内符号<之间没有空白。

同样，该问题只存在于XAML标记中。如果通过代码设置文本框中的文本，所有空格都将被使用。

#### 3.7 事件

到目前，介绍的所有特性都被映射为属性。然而，特性也可用于关联事件处理程序。用于关联事件处理程序的语法为：事件名="事件处理程序方法名"。

例如，Button控件提供了Click事件。可使用如下所示的标记关联事件处理程序：

```xml
<Button ... Click="cmdAnswer_Click">
```

上面的标记假定在代码隐藏类中有名为cmdAnswer_Click的方法。事件处理程序必须具有正确的签名。下面是一个符合要求的方法：

```csharp
private void cmdAnswer_Click(object sender, RoutedEventArgs e)
{
	this.Cursor = Cursors.wait;

	//Dramatic delay...
	System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));

	AnswerGenerator generator = new AnswerGenerator();
	txtAnswer.Text = generator.GetRandomAnswer(txtQuestion.Text);
	this.Cursor = null;
}
```

WPF中的事件模型和其他类型的.NET应用程序的事件模型不同。WPF事件模型依赖于事件。

许多情况下，将使用特性为同一元素设置属性和关联事件处理程序。WPF总是遵循以下顺序：首先设置Name属性（如果设置的话），然后关联任意事件处理程序，最后设置其他属性。这意味着，所有对属性变化做出响应的事件处理程序在第一次设置属性时都会被触发。

## 4、使用其他名称空间中的类型 ##

前面已经介绍了如何在XAML中使用WPF中的类来创建基本的用户界面。但XAML是实例化.NET对象的通用方法，包括那些位于其他非WPF名称空间以及自己创建的名称空间中的对象。

创建那些不是用于XAML窗口中显示的对象听起来像是多余的，但在很多情况下这是需要的。一个例子是，当使用数据绑定并希望在某个控件上显示从其他对象提取的信息时。另一个例子是希望使用非WPF对象为WPF对象设置属性时。

例如可使用数据对象填充WPF的ListBox控件。ListBox控件将调用ToString()方法来获取文本，以便在列表中显示每个条目。

为使用未在WPF名称空间中定义的类，需要将.NET名称空间映射到XML名称空间。XAML有一种特殊的语法可以用于完成这一工作，该语法如下所示：

```xml
xmlns:Prefix="clr-namespace:Namespace;assemblyName"
```

通常，在XAML文档的根元素中，在紧随声明WPF和XAML名称空间的特性之后放置这个名称空间。还需要使用适当的信息填充三个部分，这三个部分的含义如下：

* Prefix是希望在XAML标记中用于指示名称空间的XML前缀。例如XAML语言使用x前缀。
* Namespace是完全限定的.NET名称空间的名称。
* AssemblyName是声明类型的程序集，没有.dll扩展名。这个程序集必须在项目中引用。如果希望使用项目程序集，可忽略这一部分。

例如：

下面的标记演示了如何访问System名称空间中的基本类型，并将其映射为前缀sys：

```xml
xmlns:sys="clr-namespace:System;assembly=mscorlib"
```

下面的标记演示了如何访问当前项目在MyProject名称空间中声明的类型，并将它们映射为前缀local：

```xml
xmlns:local="clr-namespace:MyNamespace"
```

那么创建其中一个名称空间的类的实例，可使用名称空间前缀：

```xml
<local:MyObject ...></local:MyObject>
```

理想情况是，希望在XAML中使用的每个类都有无参构造函数。如果具有无参构造函数，XAML解析器就可创建对应的对象，设置其属性，并关联所提供的任何事件处理程序。XAML不支持有参构造函数，而且WPF中的所有元素都包含无参构造函数。此外，需要能够使用公共属性设置您所期望的所有细节。

XAML不允许设置公共字段或调用方法。

如果想要使用的类没有无参构造函数，就有些限制。如果试图创建简单的基本类型，如字符串、日期或数字类型，可提供数据的字符串表示形式作为标签中的内容。XAML解析器接着将使用类型转换器将字符串转换为合适的对象。

下面列举一个使用DateTime结构的例子：

```xml
<sys:DateTime>10/30/2010 4:30 PM</sys:DateTime>
```

因为DateTime类使用TypeConverter特性将自身关联到DateTimeConverter类，所以上面的标记可以奏效。DateTimeConverter类知道这个字符串是合法的DateTime对象，并对其进行转换。当使用该技术时，不能使用特性为您的对象设置任何属性。

下面我们再写个例子，将上面这些内容融合在一起：

将sys前缀映射到System名称空间，并使用System名称空间创建三个DateTime对象，然后用这三个DateTime对象填充一个列表：

```xml
<Window x:Class="WindowsApplication1.Window1"
  xmlns="http://schemes.microsoft.com/winfx/2006/xaml/presentation"
  xmlns:x="http://schemes.microsoft.com/winfx/2006/xaml"
  xmlns:sys="clr-namespace:System;assembly=macorlib">
  <ListBox>
	<ListBoxItem>
	  <sys:DateTime>10/13/2013 4:30 PM</sys:DateTime>
	</ListBoxItem>
    <ListBoxItem>
	  <sys:DateTime>10/29/2013 12:30 PM</sys:DateTime>
	</ListBoxItem>
    <ListBoxItem>
	  <sys:DateTime>10/13/2013 2:30 PM</sys:DateTime>
	</ListBoxItem>
  </ListBox>
</Window>
```

## 5、加载和编译XAML ##

前面已经介绍过，尽管XAML和WPF这两种技术具有相互补充的作用，但它们也是相互独立的。因此，完全可以创建不使用XAML的WPF应用程序。

总之，可以使用三种不同的编码方式来创建WPF应用程序：

* **只使用代码。**这是在Visual Studio中创建Windows窗口应用程序使用的传统方式。它通过代码语句生成用户界面。
* **使用代码和未经编译的标记（XAML）。**这种具体方式对于某些特殊情况是很有意义的，例如创建高度动态化的用户界面。这种方式在运行时使用System.Windows.Markup名称空间中的XamlReader类，从XAML文件中加载部分用户界面。
* **使用代码和编译过的标记（BAML）。**对于WPF而言这是一种更好的方式，也是Visual Studio支持的一种方式。这种方式为每个窗口创建一个XAML模板，这个XAML模板被编译为BAML，并嵌入到最终的程序集中。编译过的BAML在运行时被提取出来，用于重新生成用户界面。

接下来我们就深入分析下这几种方式和其工作原理。你将看到如何在浏览器中打开松散的、没有使用任何代码的XAML文件。

#### 5.1 只使用代码

对于编写WPF应用程序，只使用代码进行开发而不使用任何XAML的做法并不常见。只使用代码进行开发的明显缺点在于，可能会使编写WPF程序成为极端乏味的工作。WPF控件没有包含参数化的构造函数，因此即使为窗口添加一个简单按钮也需要编写几行代码。

只使用代码进行开发的一个潜在优势是可以随意定制应用程序。例如，可以根据数据库记录中的信息生成充满输入控件的窗体，或可根据当前的用户酌情添加或替换控件。需要的所有内容只不过是少量的条件逻辑。相比之下，如果使用XAML文档，它们只能作为固定不变的资源嵌入到程序集中。

#### 5.2 使用代码和未经编译的XAML

使用XAML最有趣的方式之一是使用XamlReader类随时解析它。例如，假设开始时在一个名为Window1.xaml的文件中使用下面的XAML内容：

```xml
<DockPanel xmlns="schemas.microsoft.com/winfx/2006/xaml/presentation">
  <Button Name="button1" Margin="30">Please click me.</Button>
</DockPanel>
```

在运行时，可将上面的内容加载到一个已经存在的窗口中，以便创建一个一模一样的窗口。

下面是完成这一工作的代码：

```csharp
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.IO;

public class Window1 : Window
{
	private Button button1;

	public Window1()
	{
		InitializeComponent();
	}

	public Window1(string xamlFile)
	{
		//Configure the form.
		this.Width = this.Height = 285;
		this.Left = this.Top = 100;
		this.Title ="Dynamically Loaded XAML";
	
		//Get the XAML content from an external file.
		DependencyObject rootElement;
		using (FileStream fs = new FileStream(xamlFile, FileMode.Open))
		{
			rootElement = (DependencyObject)XamlReader.Load(fs);
		}

		//Insert the markup into this window
		this.Content = rootElement;

		//Find the control with the appropriate name.
		button1 = (Button)LogicalTreeHelper.FindLogicalNode(rootElement, "button1");

		//Wire up the event handler.
		button1.Click += button1_Click;
	}

	private void button1_Click(object sender, RoutedEventArgs e)
	{
		button1.Content = "Thank you!";
	}
}
```

在此，构造函数接收XAML文件名作为参数，然后构造函数打开一个FileStream对象，并使用XamlReader.Load()方法将这个文件中的内容转换成DependencyObject对象，DependencyObject是所有WPF控件继承的基类。DependencyObject对象可放在任意类型的容器中，在这个示例中它被用作了整个窗口的内容。

为操纵元素，需要在动态加载的内容中查找相应的空间对象。LogicalTreeHelper类可到达该目的，因为它具有查找一棵完整控件对象树的能力，它可以查找所需的许多层，直至找到具有指定名称的对象。然后将一个事件处理程序关联到Button.Click事件。

另一种方法是使用FrameworkElement.FindName()方法。

在这个示例中，Window1.xaml文件和可执行的应用程序位于同一文件夹中，并一同发布。然而，尽管该文件没有被编译为应用程序的一部分，但仍可以将其添加到VS项目中。这样可以更方便的管理文件，并使用VS设计用户界面。

如果使用这种方法，确保松散的XAML文件不会像传统的XAML文件那样被编译或嵌入到项目中。将文件添加到项目后，在Solution Explorer中选中该文件，然后设置Build Action为None，并将Copy to Output Directory设置为Copy Always。

显然，先将XAML编辑为BAML，再在运行时加载BAML，比动态加载XAML的效率高，当用户界面比较复杂时尤其如此。然而这种编码模式为构建动态的用户界面提供了多种可能。

#### 5.3 使用代码和编译过的XAML

通过在前面写的示例，我们已经看到了XAML的最常用方式，并在本章节中通篇进行了分析。

这是VS的使用方法，它具有几个在本章中已经介绍过的优点：

* 有些内容可以自动生成。不需要使用LogicalTreeHelper类进行查找，也不需要在代码中关联事件处理程序。
* 在运行时读取BAML比读取XAML的速度要快。
* 部署更简单。因为BAML是一个或多个资源嵌入到程序集中，不会丢失。
* 可在其他程序中编辑XAML文件，例如设计工具。

当编译WPF应用程序时，VS使用分为两个阶段的编译处理过程。第一阶段将XAML文件编译为BAML。同时，使用选择的语言为窗口创建部分类。

部分类包含如下三部分内容：

* 窗口中所有控件的字段。
* 从程序集中加载BAML的代码，由此创建对象树。当构造函数调用InitializeComponent()方法时将发生这种情况。
* 将恰当的控件对象指定给各个字段以及连接所有事件处理程序的代码。该过程是在名为Connect()的方法中完成的，BAML解析器在每次发现一个已经命名的对象时调用该方法一次。

部分类不包含实例化和初始化控件的代码，因为这项任务由WPF引擎在使用Application.LoadComponent()方法处理BAML时执行。

当XAML到BAML编译阶段结束后，VS使用合适的语言编译器来编译代码和生成的部分类文件。对于C#应用程序而言，使用csc.exe编译处理这一任务。编译过的代码会变成单个程序集，而且每个窗口的BAML都作为独立资源被嵌入到程序集中。

#### 5.4 只使用XAML

前面介绍了如何在基于代码的应用程序中使用XAML。.NET开发人员的大部分工作时间都将花费在这个方面。但也可能使用XAML文件而不创建任何代码，这称为松散的XAML文件。可以在浏览器中直接打开松散的XAML文件。

到目前为止，创建松散的XAML文件没什么用处。当浏览XAML时发现几个完全声明的特性，这些特性包括动画、触发器、数据绑定和链接等。使用这些特性，可构建一些非常简单的没有代码的XAML文件。虽然不是完整的应用程序，但可以完成比静态HTML页面更多的工作。

