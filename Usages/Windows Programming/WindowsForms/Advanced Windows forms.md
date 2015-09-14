本节笔记用于windows窗体程序的高级功能。

# Windows窗体的高级功能 #

我们知道一个Windows程序界面很少只包含一个对话框，这些应用程序一般使用单一文档界面（Single Document Interface，SDI）或多文档界面（Multiple Document Interface，MDI），这两种类型的应用程序通常会使用大量的菜单和工具栏，本章主要讨论它们。

本章首先介绍菜单控件，再介绍工具栏，说明如何把工具栏上的按钮和特定的菜单项链接起来，或者把特定的菜单项与工具栏上的按钮链接起来。接着创建SDI和MDI应用程序，主要讨论MDI应用程序，因为SDI基本上是MDI应用程序的子集。

我们前面接触的控件都是.NET Framework提供的控件。这些控件非常强大，提供了很多功能。但有时候它们还不够，所以需要创建定制的控件，本章最后就是介绍如何创建定义控件。

## 一、菜单和工具栏 ##

几乎每个Windows应用都包含菜单和工具栏。在为Windows操作系统写的程序中，菜单和工具栏可能是不可或缺的重要部分。为了帮助用户创建应用程序的菜单，VS提供了两个空间，使用它们不必做太多的工作，就可以快速创建外观类似于VS的菜单和工具栏。

### 1、两个实质一样的控件

下面介绍的这两个空间是VS2005的新增控件，它们为开发人员提供了很多强大的功能。使用这两个空间可以创建出具有工具栏和菜单的应用程序。以前需要几个星期才能完成创建的应用程序，现在变成区区数秒即可完成的简单任务。

我们要使用的控件包含在后缀为Strip的控件系列中，分别是ToolStrip、MenuStrip和StatusStrip。StatusStrip后面会详细介绍。从它们最纯粹的形式来看，ToolStrip和MenuStrip实际上是相同的控件，因为MenuStrip直接派生于ToolStrip。也就是说，ToolStrip能做的工作，MenuStrip也能完成。显然一起使用工作会完成的更好。

### 2、使用MenuStrip控件

除了MenuStrip控件之外，还有许多控件可以填充菜单。3个最常见的控件是ToolStripMenuItem、ToolStripDropDown和ToolStripSeparator。这些控件表示查看菜单或工具栏中的水平或垂直分割线。

除了MenuStrip外，还有一种菜单ContextMenuStrip。当用户右击一项时，关联菜单就会显示出来，它通常显示与该项相关的信息。

### 3、手工创建菜单

从工具箱中把Menu控件拖放到设计界面上时，该控件会位于窗体和控件盘上，而且可以直接在窗体上编辑。要创建新的菜单项，只需要把指针放在Type Here框上。

在突出显示的框中输入菜单的标题，在要用作该菜单项快捷键字符的字母前加上一个宏字符&，在菜单项中，该字母显示为下划线形式，可以按下Alt键和该字母键来选择该菜单项。

注意，可以在一个菜单中用同一快捷键字符创建好几个菜单项，其规则是每个弹出菜单只能将该字母使用一次。如果不小心把同一个快捷键字符赋予了同一个弹出菜单中的多个菜单项，则只有最靠近控件顶部的那个菜单项会相应该字符。

选择一项，控件就会自动在当前项的下面和右边显示一些项。给这两个控件输入标题，就创建了与开始时选中的项相关的一个新项，这就是创建下拉菜单的方式。

要创建水平线，把菜单分成组，必须使用ToolStripSeparator控件，而不是ToolStripMenuItem控件，但不需要插入另一个控件。还可以键入一个短横线（-），作为该项的标题。VS会自动假定该项是一个分隔符，改变控件的类型。

### 4、ToolTripMenuItem控件的其他属性

ToolStripMenuItem有另外几个属性，在创建菜单时应了解这些属性。下表不完整，如果想查阅完整列表，可参阅.NET Framework SDK文档说明。

属性 | 说明
:---:|:---:
Checked | 表示菜单是否被选中
CheckOnClick | 这个属性是true时，如果菜单项文本左边的复选框没有打上标记，就打上标记，如果该复选框已打上标记，就去除该标记，否则，该标记就被一个图像替代，使用checked属性确定菜单项的状态
Enabled | 把Enabled设置为false，菜单项就会灰显，不能被选中
DropDownItems | 这个属性返回一个项集合，用作与菜单项相关的下拉菜单

### 5、给菜单添加功能

只需在单击菜单时执行某些操作即可。这个操作取决于用户。为了响应用户做出的选择，就应为ToolStripMenuItems发送的两个事件之一提供处理程序。

事件名称 | 说明
:------:|:---:
Click | 在用户单击菜单项时，引发该事件。大多数情况下这就是要响应的事件
CheckedChanged | 当单击带CheckOnClick属性的菜单项时，引发这个事件

## 二、工具栏 ##

通过菜单可以访问应用程序中的大多数功能，把一些菜单项放在工具栏中和放在菜单中有相同的作用。工具栏提供了单击访问程序中常用功能（如open和Save）的方式。

工具栏上的按钮通常包图片，不包含文本，但它可以既包含图片又包含文本。像word中工具栏就不包含文本，在IE中的工具栏包含文本。除了按钮之外，工具栏上偶尔也会有组合框和文本框。如果把鼠标指针停留在工具栏的一个按钮上，就会显示一个工具提示，给出该按钮的用途信息，特别是只显示图标时，这是很有帮助的。

ToolStrip与MenuStrip一样，也具有专业化的外观和操作方式。在用户查看工具栏时，希望能把它移动到自己希望的任意位置上。ToolStrip就允许用户这么做。

第一次把ToolStrip添加到窗体的设计界面上时，它看起来非常类似于前面的MenuStrip，但存在两个区别：ToolStrip的最左边有4个垂直排列的点，这与VS中的菜单相同。这些点表示工具栏可以移动，也可以停靠在父应用程序窗口中。第二个区别是在默认情况下，工具栏显示的是图像，而不是文本，所以工具栏上项的默认控件是按钮。工具栏显示的下拉菜单允许选择菜单项的类型。

ToolStrip与MenuStrip完全相同的一个方面是，Action窗口包含Insert Standard Items链接。单击这个链接，不会得到和MenuStrip相同的菜单项数，而会获得New、Open、Save、Print、Cut、Copy、Paste和Help等按钮。

下面先介绍ToolStrip的一些属性和用于填充它的控件。

### 1、ToolTrip控件的属性

ToolStrip控件的属性控制和管理着控件的显示位置和显式方式。这个控件是前面介绍的MenuStrip控件的基础，所以它们具有许多相同的属性。下面只列出了几个比较重要的属性，如果需要完整的列表，可参阅.NET Framwork SDK文档说明。

属性 | 说明
:---:|:---:
GripStyle | 控制4个垂直排列的点是否显示在工具栏的最左边。隐藏手柄后，用户就不能移动工具栏了
LayoutStyle | 控制工具栏上的项如何显示，默认为水平显示
Items | 包含工具栏中所有项的集合
ShowItemToolTip | 确定是否显示工具栏上某项的工具提示
Stretch | 默认情况下，工具栏比包含在其中的项略宽或略高。如果把Stretch属性设置为true，工具栏就会占据其容器的总长

### 2、ToolTrip的项

在ToolStrip中可以使用许多控件。前面提到，工具栏应能包含按钮、组合框和文本框。除了与这些对应的控件之外，工具栏还可以包含其他控件。如下面所示。

控件 | 说明
:---:|:---:
ToolStripButton | 表示一个按钮，用于带文本和不带文本的按钮
ToolStripLabel | 表示一个标签。这个控件还可以显示图像，也就是说，这个控件可以用于显示一个静态图像，放在不显示其本身信息的另一个控件上面，例如文本框或组合框
ToolStripSplitButton | 显示一个右端带有下拉按钮的按钮，单击该下拉按钮，就会在它的下面显示一个菜单，如果单击控件的按钮部分，该菜单不会打开
ToolStripDropDownButton | 类似于ToolStripSplitButton，唯一的区别是去除了下拉按钮，代之以下拉数组图像，单击控件的任一部分，都会打开其菜单部分 
ToolStripComboBox | 显示一个组合框
ToolStripProgressBar | 在工具栏上嵌入一个进度条
ToolStripTextBox | 显示一个文本框
ToolStripSeparator | 前面在菜单示例中见过这个控件，它为各个项创建水平或垂直分隔符

### 3、StatusStrip控件

Strip控件系列中最后一个控件是StatusStrip。这个控件在许多应用程序中表示对话框底部的一栏，它通常用于显示应用程序当前状态的简短信息，例如在word中键入文本时，word会在状态栏中显示当前的页面、列和行等。

StatusStrip派生于ToolStrip，在把这个控件拖放到窗体上时，读者应很熟悉其视图。在StatusStrip中可以使用前面介绍的4个控件中的3个：ToolStripDropDownButton、ToolStripProgressBar、ToolStripSplitButton。还有一个控件是StatusStrip专用的，即StatusStripStatusLabel，它也是一个默认项。

### 4、StatusStripStatusLabel的属性

StatusStripStatusLabel使用文本和图像向用户显示应用程序当前状态的信息。标签是一个非常简单的控件，没有太多的属性，下面介绍两个属性不是专门用于标签的，但它们十分有用。

属性 | 值
:---:|:---:
AutoSize | AutoSize在默认情况下是打开的，这不是非常直观，因为在改变状态栏上标签的文本时，不希望该标签来回移动，除非标签上的信息是静态的，否则总是应把这个属性改为false
DoubleClickEnable | 在这个属性中，可以指定是否引发DoubleClick事件。也就是说，用户可以在应用程序的另一个地方修改信息，例如让用户双击Bold的面板，在文本中启用或禁用粗体格式

## 三、SDI和MDI应用程序 ##

传统上可以为Windows编写三种应用程序，它们是：

* **基于对话框的应用程序：**它们向用户显示一个对话框，该对话框提供了所有的功能。
* **单一文档界面(SDI)：**这些应用程序向用户显示一个菜单、一个或多个工具栏和一个窗口，在该窗口中，用户可以执行任务。
* **多文档界面(MDI)：**这些应用程序的执行方式与SDI相同，但可以同时打开多个窗口。

基于对话框的应用程序通常用途比较单一，它们可以完成用户输入量非常少的特定任务，或者专门处理某一类型的数据。这种应用程序的一个示例是Windows中的计算器。

单一文档界面通常用于完成一个特定任务，因为它允许用户把要处理的单一文档加载到应用程序中。但这个任务通常涉及到许多用户的交互操作，用户也常常希望能保存或加载工作的结果。SDI应用程序的示例是写字板和画图，它们都是Windows附带的程序。

但一次只能打开一个文档，所以如果用户要打开第二个文档，就必须打开一个新的SDI应用程序实例，它与第一个实例没有关系，对一个实例的任何配置都不会影响第二个实例。例如在画图的一个实例中，可以把绘图颜色设置为红色，如果打开图画的第二个实例，绘图颜色仍是默认的黑色。

多文档界面与SDI应用程序极为相似，但它可以在任意时刻在不同窗口保存多个已打开的文档。MDI的标识符包含在菜单栏右边的Windows菜单中，该菜单在Help的前面。VS就是一个MDI程序。VS的每个设计器和编辑器都在同一个应用程序中打开，菜单和工具栏会自动调整，以匹配当前的选择。

## 四、生成MDI应用程序 ##

创建MDI会涉及到什么问题？

* 首先，希望用户能完成的任务应是需要一次打开多个文档的任务。例如，文本编辑器或文本查看器。
* 第二，应在应用程序中提供工具栏来完成最常见的任务，例如设置字体样式、加载和保存文档等。
* 第三，应提供一个包含Window菜单项的菜单，让用户可以重新定位打开的窗口（平铺和层叠）。显示所有已打开窗口的列表。

MDI应用程序的另一个功能是如果打开了一个窗口，该窗口包含一个菜单，则该菜单就应集成到应用程序的主菜单上。

MDI应用程序至少要由两个截然不同的窗口组成。第一个窗口叫做MDI容器（Container），可以在容器中显示的窗口叫做MDI子窗口。

## 五、创建控件 ##

有时VS提供的控件不能满足用户的需求。原因有多方面，控件不能以希望的方式绘制自己，或者控件在某个方面有限制，或者需要的控件不存在。为此，Microsoft提供了创建满足需要的控件的方式。VS提供了一个项目类型Windows Control Library，使用它可以创建自己的控件。

可以开发两种不同类型的自定义控件：

* 用户或组合控件： 这种控件是根据现有控件的功能创建一个新控件。这类控件一般用于把控件的用户界面和功能封装在一起，或者把几个其他控件组合在一起，从而改善控件的界面。
* 定制控件： 当没有控件可以满足要求时，就创建这类控件，即从头创建控件。它要自己绘出整个用户界面。在创建控件的过程中没有现有的控件可以使用。当想要创建的控件的用户界面与其他可用的控件都不同时，一般需要创建这样的控件。

本章这里只讨论用户控件，因为从头设计和绘制控件已经超出了我们当下的范畴。

用户控件继承于类System.Windows.Forms.UserControl。这个基类提供的控件具有.NET中控件应具有的所有基本功能——用户只需创建控件即可。实际上，任何对象都可以创建为一个控件，包括设计俏皮的标签乃至功能全面的网格控件。

注：用户控件派生于System.Windows.Forms.UserControl类，而定制控件派生于System.Windows.Forms.Control类。

在处理控件时，要考虑几个问题。如果控件不满足这些条件，人们就不会使用它。这些条件是：

* 在设计期间，控件的操作方式应尽可能接近运行期间的操作方式。如果将一个标签和一个文本框合并，创建一个LabelTextBox控件，标签和文本框就都要在设计间显示出来，为标签输入的文本也要在设计期间显示出来。这里说的很简单，但在较复杂的情况下，就会出问题，就需要采取一种折中的方法。
* 应可以在窗体设计器中按合理方式访问控件的属性。例如ImageList控件显示了一个对话框，用户可以在该对话框中浏览要包含的图像，导入了图像后，它们就显示在对话框的一个列表中。

在后面，我们会通过例子说明如何创建控件。

我们这里描述下这个控件LabelTextBox，后面还需要用到。

从这个控件的名称可以看出，这个控件是使用两个现有控件来创建的。一步执行一个任务在Windows编程中非常常见：给窗体添加一个标签，再在该窗体中添加一个文本框，把文本框和标签的位置关联起来。下面看看这个控件的用户可以执行什么操作：

* 用户可以把文本框放在标签的右边或下边，如果文本框放在标签的右边，就可以指定该控件的左边界与文本框之间的固定距离，使文本框对齐。
* 用户应能使用文本框和标签的常用属性和事件。

### 1、调试用户控件

调试用户控件与调试Windows应用程序大不相同。一般情况下，可以在某个位置添加断点，按下F5，看看发生什么情况。

### 2、扩展LabelTextbox控件

最后，准备测试控件的属性。注意在LabelTextBox控件添加到窗体上时，其中的控件会移动到正确的位置上。因为把Position属性的默认值设置为Right，所以文本框位于标签的旁边，把Position属性设置为Below，文本框会移动到标签之下。

#### 2.1、添加更多属性

现在还不能对该控件进行什么操作，因为它还不能改变标签和文本框中的文本。下面添加两个属性：LabelText和TextBoxText。添加这些属性的方式与添加前两个属性的方式相同，也是打开项目，添加如下代码：

```csharp
public string LabelText
{
	get {return labelCaption.Text; }
	set
	{
		labelCaption.Text = labelText =value;
		MoveControls();
	}
}

public string TextboxText
{
	get {return textBoxText.Text; }
	set 
	{
		textBoxText.Text = value;
	}
}
```

还需要声明成员变量labelText来保存文本：

```csharp
private string mLabelText = "";

public ctlLabelTextbox()
{
}
```

如果要插入文本，就把文本赋给标签和文本框控件的Text属性，返回Text属性的值。如果改变了标签的文本，需要调用MoveControls()，因为标签文本可能会影响文本框的位置。另一方面，插入到文本框中的文本不会使控件移动，如果文本比文本框长，超出文本框的部分就不会显示出来。

最后，必须修改Load事件：

```csharp
private void otlLabelTextbox_Load(object sender, EventArgs e)
{
	labelCaption.Text = labelText;
	Height = textBoxText.Height > labelCaption.Height ?
		textBoxText.Height : labelCaption.Height;
	MoveControls();
}
```

Load事件把LabelCaption控件的文本设置为属性的值。这样，设计期间和运行期间显示的文本就是相同的。

#### 2.2、添加更多事件处理程序

现在该考虑控件应提供的事件了。因为该控件派生于UserControl类，所以继承了许多无需加以处理的功能。但有许多事件我们不希望以标准的方式交给用户。如KeyDown、KeyPress、KeyUp事件。需要修改这些事件的原因是，用户希望在文本框中按下一个键时，就引发这些事件。现在只有在控件本身获得焦点，且用户按下一个键时，才会引发这些事件。

要改变其操作方式，必须处理文本框引发的事件，把它们发送给用户。给文本框添加KeyDown、KeyUp和KeyPress事件，并输入下面代码：

```csharp
private void textBoxText_KeyDown(object sender, KeyEventArgs e)
{
	OnKeyDown(e);
}

private void textBoxText_KeyPress(object sender, KeyEventArgs e)
{
	OnKeyPress(e);
}

private void textBoxText_KeyUp(object sender, KeyEventArgs e)
{
	OnKeyUp(e);
}
```

调用OnKeyXXX方法会执行订阅事件的对应方法。

#### 2.3、添加定制的事件处理程序

在创建一个基类中不存在的事件时，需要做更多的工作。下面创建一个事件PositionChanged，当Position属性改变时，将引发该事件。为了创建这个事件，需要做3件事：

* 需要一个合适的委托，用于调用用户赋给事件的方法。
* 用户必须把一个方法赋给事件，以订阅该事件。
* 必须调用用户赋给事件的方法。

要使用的委托是由.NET Framwork提供的EventHandler委托。这是一个特殊的委托，它由其关键字event声明。下面的代码声明了一个事件，允许用户订阅该事件：

```csharp
public event System.EventHandler PositionChanged;

public ctlLabelTextbox()
{
}
```

现在只剩下引发该事件了。当改变Position属性时，将引发该事件。所以在Position属性的set存取器中引发该事件：

```csharp
public PositionEnum Position
{
	get {return position;}
	set
	{
		position = value;
		MoveControls();
		if (PositionChanged != null)
			PositionChanged(this, new EventArgs());
	}
}
```

首先，确保检查PositionChanged是否为null，看看有没有订阅者。如果没有，就调用方法。

可以像订阅其他事件那样订阅新的定制事件，但这里有一个小问题：该事件在事件窗口中显示之前，必须先生成控件。只有生成了控件，才能在LabelTextBoxText项目的窗体中选择控件，在属性面板的Events部分双击PositionChanged事件。接着给事件处理程序添加如下代码：

```csharp
private void ctlLabelTextbox1_PositionChanged(object sender, EventArgs e)
{
	MessageBox.show("Changed");
}
```

该定制事件处理程序什么都不会做，它只是说明位置改变了。

最后，在窗体上添加一个按钮，双击它，给项目添加该按钮的Click事件处理程序，添加如下代码：

```csharp
private void buttonToggle_Click(object sender, EventArgs e)
{
	ctlLabelTextbox1.Position = ctlLabelTextbox1.Position ==
	LabelTextbox.ctlLabelTextbox.PositionEnum.Right ?
	LabelTextbox.ctlLabelTextbox.PositionEnum.Below :
	LabelTextbox.ctlLabelTextbox.PositionEnum.Right;
}
```

当运行应用程序时，就可以在运行期间改变文本框的位置。每次移动文本框，都会触发事件PositionChanged，显示一个信息框。

这个示例基本上到此就结束了，还可以继续进行细化。