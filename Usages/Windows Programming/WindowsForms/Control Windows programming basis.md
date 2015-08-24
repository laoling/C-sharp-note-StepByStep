这篇笔记我们主要用于记录windows窗体程序开发的基础知识。

# Windows编程基础 #

Windows窗体程序开发很长一段时间内都保持着这样一种开发模式，使用直观的窗体设计器创建用户界面，其中预置好了所需要的各种控件，将控件拖放到设计器中，双击打开控件，添加控件处理程序。这种开发模式也适用于C#开发人员。我们就先从这种程序建立方法开始界面开发。

这里我们只介绍最常见的控件，包括标签、文本框、列表视图、选项卡控件等。

## 一、控件 ##

在使用Windows窗体时，就是在使用System.Windows.Forms命名空间。这个命名空间使用using指令包含在存储Form类的一个文件中。.NET中的大多数控件都派生于System.Windows.Forms.Control类。这个类定义了控件的基本功能，这就是控件中的许多属性和事件都相同的原因。许多类本身就是其他控件的基类。

### 1、属性

所有控件都有许多属性，用于处理控件的操作。大多数控件的基类都是System.Windows.Forms.Control，它有许多属性，其他控件要么直接继承了这些属性，要么重写它们以便提供某些定制的操作。

下面我们列出Control类最常见的一些属性。这些属性在本章介绍各个控件中都有，所以后面就不再详细解释，除非属性的操作对于某个控件来说进行了改变。注意这个列表并不完整，如果要查看该类的所有属性，请参阅.NET Framework SDK 文档说明。

 属性 | 说明 
:----:|:---:
Anchor | 指定当控件的容器大小发生变化时，该控件如何响应。参见下节来了解对这个属性的详细解释
BackColor | 控件的背景色
Bottom | 指定控件底部距窗口顶部的距离。这与指定控件的高度不同
Dock | 使控件停靠在容器的边界上。参见下面对这个属性的详细解释
Enabled | 把Enabled设置为true通常表示该控件可以接收用户的输入。把Enabled设置为false通常表示不能接收用户的输入
ForeColor | 控件的前景色
Height | 控件底部到顶部的距离
Left | 控件的左边界距其容器左边界的距离
Name | 控件的名称。这个名称可以在代码中用于引用该控件
Parent | 控件的父控件
Right | 控件的右边界距其容器左边界的距离
TabIndex | 控件在容器中的标签顺序号
TabStop | 指定是否可以用Tab键访问控件
Tag | 这个值通常不由控件本身使用，而是在控件中存储该控件的信息。当通过Windows窗体设计器给这个属性赋值时，就只能给它赋一个字符串值
Text | 保存与该控件相关的文本
Top | 控件顶部距其容器顶部的距离
Visible | 指定控件是否在运行期间可见
Width | 控件的宽度

### 2、控件的定位、停靠和对齐

在Visual Studio中，窗体设计器使用一个清洁的界面，并使用捕捉线来定位控件。实际操作在IDE中尝试。

### 3、Anchor和Dock属性

在设计窗体时，这两个属性特别有用。如果用户认为改变窗口的大小并非小事，应确保窗口看起来不显得很乱，在以前只有编写许多代码行才能到达这个目的。许多程序解决这个问题时，都是禁止给窗口重新设置大小，这显然是解决问题最简单的方法，但不是最好的方法。.NET引入了Anchor和Dock属性，就是为了在不编写任何代码的情况下解决这个问题。

Anchor属性指定在用户重新设置窗口的大小时控件该如何响应。可以指定如果控件重新设置了大小，就根据控件的边界合理地锁定它，或者其大小不变，但根据窗口的边界来锚定它的位置。

Dock属性指定控件应停靠在容器的边框上。如果用户重新设置了窗口的大小，该控件将继续停放在窗口的边框上。例如如果指定控件停靠在容器的底部边界上，则无论窗口的大小如何改变，该控件都将改变大小，或移动位置，确保总是位于屏幕的底部。

### 4、事件

前面我们介绍过事件的含义和用法。这里我们介绍事件的特定类型，即Windows窗体控件生成的控件。这些事件通常与用户的操作相关。例如在用户单击按钮时，该按钮就会生成一个事件，说明发生了什么。处理事件就是程序员为该按钮提供某些功能的方式。

Control类定义了本章所用控件的一些比较常见的事件。下面描述了许多这类事件。这个表仅列出了最常见的事件：如果需要查看完整的列表，请查阅.NET Framework SDK文档说明。

事件 | 说明
:----:|:---:
Click | 在单击控件时引发。在某些情况下，这个事件也会在用户按下回车键时引发
DoubleClick | 在双击控件时引发。处理某些控件上的Click事件，如Button控件，表示永远不会调用DoubleClick事件
DragDrop | 在完成拖放操作时引发。换言之，当一个对象被拖到控件上，然后用户释放鼠标按钮后，引发该事件
DragEnter | 在被拖动的对象进入控件的边界时引发
DragLeave | 在被拖动的对象移除控件的边界时引发
DragOver | 在被拖动的对象放在控件上时引发
KeyDown | 当控件有焦点时，按下一个键时引发该事件，这个事件总是在KeyPress和KeyUp之前引发
KeyPress | 当控件有焦点时，按下一个键时发生该事件，这个事件总是在KeyDown之后、KeyUp之前引发。KeyDown和KeyPress的区别是KeyDown传送被按下的键的键盘码，而KeyPress传送被按下的键的char值
KeyUp | 当控件有焦点时，释放一个键时发生该事件，这个事件总是在KeyDown和KeyPress之后引发
GotFocus | 在控件接收焦点时引发。不要用这个事件执行控件的有效性验证，而应使用Validating和Validated
LostFocus | 在控件失去焦点时引发。不要用这个事件执行控件的有效性验证，而应使用Validating和Validated
MouseDown | 在鼠标指针指向一个控件，且鼠标按钮被按下时引发。这与Click事件不同，因为在按钮被按下之后，且未被释放之前引发MouseDown
MouseMove | 在鼠标滑过控件时引发
MouseUp | 在鼠标指针位于控件上，且鼠标按钮被释放时引发
Paint | 绘制控件时引发
Validated | 当控件的CausesValidation属性设置为true，且该控件获得焦点时，引发该事件，它在Validating事件之后发生，表示验证已经完成
Validating | 当控件的CausesValidation属性设置为true，且该控件获得焦点时，引发该事件，注意被验证的控件是正在失去焦点的控件，而不是正在获得焦点的控件

后面的示例都使用相同的格式，即首先创建窗体的可视化外观，选择并定位控件，再添加事件处理程序，事件处理程序包含了示例的主要工作代码。

有三种处理事件的基本方式。第一种是双击控件，进入控件默认事件的处理程序，这个事件因控件而异。如果该事件就是我们需要的事件，就可以开始编写代码。如果需要的事件与默认事件不同，有两种方法来处理这个情况。

一种方法是使用属性窗口中的Events列表。要给事件添加处理程序，只需在Events列表中双击该事件，就会生成给控件订阅该事件的代码，以及处理该事件的方法签名。另外还可以在Events列表中该事件旁边，为处理该事件的方法输入一个名称。按下回车键，就会用我们输入的名称生成一个事件处理程序。

另一个选项是自己添加订阅该事件的代码。在输入订阅该事件所需代码时，VS会检测到我们做的工作，并在代码中添加方法签名，就好像在窗体设计器中一样。

注意：这两种方式都需要两步：订阅事件和处理方法的正确签名。如果双击控件，给要处理的事件编辑默认事件的方法签名，以处理另一个事件，就会失败，因为还需要修改InitializeComponent()中的事件订阅代码，所以这种方法并不是处理特定事件的快捷方式。

下面我们讨论最常见的一个控件，即Button控件。

## 二、Button控件 ##

.NET Framework提供了一个派生于Control的类System.Windows.Forms.ButtonBase,它实现了Button空间所需的基本功能，所以程序员可以从这个类中派生，创建定制的Button控件。

System.Windows.Forms命名空间提供了三个派生于ButtonBase的控件，即Button、CheckBox和RadioButton。本节主要讨论Button控件，后面再介绍另外两个按钮。

Button控件存在于几乎所有的Windows对话框中，按钮主要用于执行3类任务：

* 用某种状态关闭对话框（如OK和Cancel按钮）。
* 给对话框上输入的数据执行操作（输入一定的条件后，单击Search）。
* 打开另一个对话框或应用程序（如Help按钮）。

对Button控件的处理是非常简单的。通常是在窗体上添加控件，再双击它，给Click事件添加代码，这对于大多数应用程序来说就足够了。

### 1、Button控件的属性

下面介绍该控件的常用属性，了解如何操作它。下面的表格列出了Button类最常用的属性，但从技术上讲，它们都是在ButtonBase基类上定义的。这里只解释最常见的属性。完整列表请参阅.NET Framework SDK文档说明。

属性 | 说明
:----:|:---:
FlatStyle | 可以用这个属性改变按钮的样式。如果把样式设置为PopUp，则该按钮就显示为平面，直到用户再把鼠标指针移动到它上面为止。此时按钮会弹出，显示为3D外观
Enabled | 这个属性派生于Control，但这里仍讨论它，因为这是一个非常重要的属性，把Enabled设置为false，则该按钮就会灰显。单击它，不会起任何作用
Image | 可以指定一个在按钮上显示的图像（位图，图标等）
ImageAlign | 指定按钮上的图像在什么地方显示

### 2、Button控件的事件

按钮最常用的事件是Click。只要用户单击了按钮，即当鼠标指向该按钮时，按下鼠标左键，再释放它，就会引发该事件。如果在按钮上单击了鼠标左键，然后把鼠标移动到其他位置，再释放鼠标，将不会引发Click事件。同样，在按钮得到焦点，且用户按下了回车键时，也会引发Click事件。如果窗体上有一个按钮，就总是要处理这个事件。

### 3、添加事件处理程序

双击English按钮，在事件处理程序中添加如下代码：

```csharp
private void buttonEnglish_Click(object sender, EventArgs e)
{
	Text = "Do you speak English?";
}
```

在VS创建处理事件的方法时，方法名是控件名、下划线和要处理事件名的组合。

对于Click事件，第一个参数Object sender包含被单击的控件。在这个示例中，控件总是由方法名来标识，但在其他情况下，许多控件可能使用同一个方法来处理事件，此时就要通过查看这个值，来确定是哪个控件调用了该方法。后面介绍的文本框控件一节说明了多个控件如何使用同一个方法。另一个参数System.EventArgs e 包含实际发生的事情的信息。在本例中，不需要这些信息。

返回设计视图，双击Danish按钮，进入这个按钮的事件处理程序，下面是代码：

```csharp
private void buttonDanish_Click(object sender, EventArgs e)
{
	Text = "Taler du danak?";
}
```

这个方法与btnEnglish_Click相同，但文本是丹麦文字。最后，以相同的方式添加OK按钮的事件处理程序。其代码有一定不同之处：

```csharp
private void buttonOK_Click(object sender,EventArgs e)
{
	Application.Exit();
}
```

使用这段代码，就可以退出应用程序。这就是第一个示例。编译这个示例，运行它，点击几个按钮，会发现对话框标题栏上的文本改变了。

## 三、Label和LinkLabel控件 ##

## 四、TextBox控件 ##

### 1、TextBox控件的属性

### 2、TextBox控件的事件

### 3、添加事件处理程序

## 五、RadioButton和CheckBox控件 ##

### 1、RadioButton控件的属性

### 2、RadioButton控件的事件

### 3、CheckBox控件的属性

### 4、CheckBox控件的事件

### 5、GroupBox控件

## 六、RichTextBox控件 ##

### 1、RichTextBox控件的属性

### 2、RichTextBox控件的事件

## 七、ListBox和CheckedListBox控件 ##

### 1、ListBox控件的属性

### 2、ListBox控件的方法

### 3、ListBox控件的事件

## 八、ListView控件 ##

### 1、ListView控件的属性

### 2、ListView控件的方法

### 3、ListView控件的事件

### 4、ListViewItem

### 5、ColumnHeader

### 6、ImageList控件

## 九、TabControl控件 ##

### 1、TabControl控件的属性

### 2、使用TabControl控件