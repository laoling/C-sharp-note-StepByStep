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

Label控件也许是最常用的控件。在任何Windows应用程序中，都可以在对话框中见到它们。标签是一个简单的控件，其用途只有一个：在窗体上显示文本。

.NET Framework包含两个标签控件，它们可以用截然不同的方式来显示：

* Label是标准的Windows标签。
* LinkLabel类似于标准标签（派生于标准标签），但以Internet链接的方式显示（超链接）。

标准的Label通常不需要添加任何事物处理代码。但它也像其他所有控件一样支持事件。对于LinkLabel控件，如果希望用户可以单击它，进入文本中显示的网页，就需要添加其他代码。

Label控件有非常多的属性。大多数属性都派生于Control，但有一些属性是新增的。下表列出了最常见的属性。如果没有特别说明，Label和LinkLabel控件中都存在这些属性。

属性 | 说明
:----:|:---:
BorderStyle | 可以指定标签边框的样式。默认为无边框
FlatStyle | 控制显示控件的方式。把这个属性设置为PopUp，表示控件一直显示为平面样式，直到用户把鼠标指针移动到该控件上面，此时，控件显示为弹起样式
Image | 指定要在标签上显示的图像（位图，图标等）
ImageAlign | 指定图像显示在标签的什么地方
LinkArea | （LinkLabel）文本中显示为链接的部分
LinkColor | （LinkLabel）链接的颜色
Links | （LinkLabel）LinkLabel可以包含多个链接。利用这个属性可以查找需要的链接。控件会跟踪显示文本中的链接，不能在设计期间使用
LinkVisited | （LinkLabel）把它设置为true，单击控件，链接就会显示另一种颜色
TextAlign | 指定文本显示在控件的什么地方
VisitedLinkColor | （LinkLabel）用户单击LinkLabel后控件的颜色

## 四、TextBox控件 ##

在希望用户输入程序员在设计阶段不知道的文本时，应使用文本框。文本框的主要用途是让用户输入文本，用户可以输入任何字符，也可以只允许用户输入数值。

.NET Framework内置了两个基本控件来提取用户输入的文本：TextBox和RichTextBox，这两个控件都派生于基类TextBoxBase，而TextBoxBase派生于Control。

TextBoxBase提供了在文本框中处理文本的基本功能，例如选择文本、剪切和从剪切板上粘贴，以及许多事件。这里不讨论派生关系，而是先介绍其中一个比较简单的控件：TextBox。在此基础上说明RichTextBox控件。

### 1、TextBox控件的属性

如前面所述，这里列出最常用的属性。

属性 | 说明
:----:|:---:
CausesValidation | 当控件的这个属性设置为true，且该控件要获得焦点时，会引发两个事件：Validating和Validated。可以处理这些事件，以便验证正在失去焦点的控件中数据的有效性，这可能使控件永远都不能获得焦点。
CharacterCasing | 这个值表示TextBox是否会改变输入的文本的大小写。可能的值有：Lower——输入的所有文本都转换为小写；Normal——不对文本进行任何转换；Upper——输入的所有文本都转换为大写
MaxLength | 这个值指定输入到TextBox中的文本的最大字符长度。把这个值设置为0，表示最大字符长度仅受限于可用的内存
Multiline | 表示该控件是否是一个多行控件。多行控件可以显示多行文本。如果将Multiline属性设置为true，通常也把WordWrap也设置为true
PasswordChar | 指定是否用密码字符替换在单行文本框中输入的字符。如果Multiline属性为true，这个属性就不起作用
ReadOnly | 这个Boolean值表示文本是否为只读
ScrollBars | 指定多行文本框是否显示滚动条
SelectedText | 在文本框中选择的文本
SelectionLength | 在文本中选择的字符数，如果这个值设置得比文本中的总字符数大，则控件会把它重新设置为字符总数减去SelectionStart的值
SelectionStart | 文本框中被选中文本的开头
WordWrap | 指定在多行文本框中，如果一行的宽度超出了控件的宽度，其文本是否应自动换行

### 2、TextBox控件的事件

在窗体上，对TextBox控件中文本进行精细的有效性验证会使一些用户很高兴，使另一些用户则会感到很生气。当用户单击了OK按钮后，对话框只验证其内容，此时用户可能非常生气。这种验证数据有效性的方式通常会显示一个信息框，告诉用户TextBox中的数据不正确。接着继续单击OK按钮，直到所有数据都正确为止。显然这不是验证数据有效性的好方法，那么我们还能怎么做呢？

答案取决于TextBox空间提供的有效性验证事件。如果要确保文本框中不输入无效的字符，或者只输入某个范围内的数值，就需要告诉控件的用户：输入的值是否有效。

TextBox控件提供了下表中的事件：

名称 | 说明
:----:|:---:
Enter Leave Validating Validated | 这四个事件按顺序引发。它们统称为焦点事件，当控件的焦点发生改变时引发，但有两个例外。Validating和Validated仅在控件接收了焦点，且属性CausesValidation属性设置为true时引发。接收焦点控件引发事件的原因是有时即使焦点改变了，我们也不希望验证控件的有效性，如Help按钮
KeyDown KeyPress KeyUp | 这三个事件称为键事件。它们可以监视和改变输入到控件中的内容。KeyDown和KeyUp接收与所按下键对应的字符。这样就可以确定是否按下了特殊键Shift或Ctrl和F1。另一方面，KeyPress接收与键对应的字符。这表示字符a和A的值不同。如果要排除某个范围内的字符。例如只允许输入数值，这样很有用
TextChanged | 只要文本框中的文本发生了改变，无论发生什么改变，都会引发该事件

### 3、添加事件处理程序

在设计视图中，双击按钮，对其他按钮重复这个过程。这样就创建出了按钮的Click事件处理程序。程序的事件处理演示我们在例子中详细说明。

## 五、RadioButton和CheckBox控件 ##

如前所述，RadioButton和CheckBox控件与Button控件有相同的基类，但它们的外观和用法大不相同。

传统上，单选按钮显示为一个标签，左边是一个圆点，该点可以是选中或未选中。在要给用户提供两个或多个互斥选项时，就可以使用单选按钮。例如询问用户的性别。

把单选按钮组合在一起，给它们创建一个逻辑单元，此时必须使用GroupBox控件或其他一些容器。首先在窗体上拖放一个组框，再把需要的RadioButton按钮放在组框中，则在任意时刻，窗体上只有一个RadioButton被选中。

传统上，CheckBox显示为一个标签，左边是一个小方框。在希望用户可以选择一个或多个选项时，就使用复选框。

下面介绍这两个控件的重要属性和事件，从RadioButton开始，然后用一个简短小示例说明它们的用法。

### 1、RadioButton控件的属性

这个按钮派生于ButtonBase，前面有一个使用按钮的示例了，所以需要描述的属性仅有几个，如表所示。完整的列表请参考.NET Framework SDK文档说明。

属性 | 说明
:----:|:---:
Appearance | RadioButton可以显示为一个标签，相应的圆点放在左边中间或右边，或者显示为标准按钮。当它显示为按钮时，控件被选中时显示为按下状态，否则显示为弹起状态
AutoCheck | 如果这个属性为true，用户单击单选按钮时，会显示一个选中标记，如果该属性为false，就必须在Click事件处理程序的代码中手工选中单选按钮
CheckAlign | 使用这个属性，可以改变单选按钮的复选框的对齐形式，默认是ContentAlignment.MiddleLeft
Checked | 表示控件的状态。如果控件有一个选中标记，它就是true，否则为false

### 2、RadioButton控件的事件

在处理RadioButton控件时，通常只使用一个事件，但还可以订阅许多其他事件。本章只介绍两个事件。介绍第二个事件的原因是它们有微妙的区别。

属性 | 说明
:----:|:---:
CheckedChanged | 当RadioButton的选中选项发生改变时，引发这个事件
Click | 每次单击RadioButton时，都会引发该事件。这与CheckedChange事件是不同的，因为连续单击RadioButton两次或多次只改变Checked属性一次（而且只有尚未选中时才如此）。而且如果被单击按钮的AutoCheck属性是false，则该按钮根本不会被选中，只引发Click事件

### 3、CheckBox控件的属性

可以想象，这个控件的属性和事件非常类似于RadioButton控件，但有两个新属性：

属性 | 说明
:----:|:---:
CheckState | 与RadioButton不同，CheckBox有三种状态：Checked、Indeterminate和Unchecked。复选框的状态时Indeterminate时，控件旁边的复选框通常是灰色的，表示复选框的当前值是无效的。或者无法确定（例如，如果选中标记表示文件的只读状态，且选中了两个文件，则其中一个文件是只读的，另一个文件不是。）
ThreeState | 这个属性为false时，用户就不能把CheckState属性改为Indeterminate。但仍可以在代码中把CheckState属性改为Indeterminate

### 4、CheckBox控件的事件

一般只使用这个控件的一两个事件。RadioButton和CheckBox都有CheckChanged事件，但效果不同。

事件 | 说明
:----:|:---:
CheckedChanged | 当复选框的Checked属性发生变化时，就引发该事件，注意在复选框中，当ThreeState属性为true时，单击复选框可能不会改变Checked属性，在复选框从Checked变为Indeterminate状态时，就会出现这个情况
CheckedStateChanged | 当CheckedState属性改变时，引发该事件。CheckedState属性的值可以是Checked和Unchecked。只要Checked属性改变了，就会引发该事件。另外当状态从Checked变为Indeterminate时，也会引发该事件

下面在使用之前，再介绍下GroupBox控件。

### 5、GroupBox控件

GroupBox控件常常用于合理地组合一组控件，如RadioButton及CheckBox控件，显示一个框架，其上有一个标题。

组框用法很简单，把它拖到窗体上，再把所需的控件拖放到组框中即可。其顺序不能颠倒，不能把组框放在已有控件上面。其结果是父控件是组框，而不是窗体，所以在任何时刻，可以选择多个RadioButton，但在组框中，一次只能选择一个RadioButton。

这里需要解释一下父控件和子控件的关系。把一个控件放在窗体上时，窗体就是该控件的父控件，所以该控件就是窗体的一个子控件。而把一个GroupBox放在窗体上时，它就成为窗体的一个子控件。而组框本身可以包含控件，所以它就是这些控件的父控件，其结果是移动GroupBox时，其中的所有控件也会随之移动。

把控件放在组框中的另一个结果是可以改变其中所有控件的某些属性，方法是在组框上设置这些属性。例如，如果要禁用组框中的所有控件，只需要把组框的Enabled属性设置为false即可。

## 六、RichTextBox控件 ##

与常用的TextBox一样，RichTextBox控件派生于TextBoxBase，所以它与TextBox共享许多功能，但许多功能是不同的。TextBox常用于从用户处获取简短的文本字符串，而RichTextBox用于显示和输出格式化的文本。它使用标准的格式化文本，称为富文本格式或RTF。

### 1、RichTextBox控件的属性

如果上面这种文本框比上节介绍的文本框更高级，我们会期待它有一些新特性。下表列出了RichTextBox的一些最常用属性。

属性 | 说明
:----:|:---:
CanRedo | 如果上一个被撤销的操作可以使用Redo重复，这个属性就是true
CanUndo | 如果可以在RichTextBox上撤销上一个操作，这个属性就是true，注意，CanUndo在TextBoxBase中定义，所以也可以用于TextBox控件
RedoActionName | 这个属性包含通过Redo方法执行的操作名称
DetectUrls | 这个属性设置为true，可以使控件检查URL，并格式化它们（像在浏览器中那样有下划线）
Rtf | 它对应于Text属性，但包含RTF格式的文本
SelectedRtf | 使用这个属性可以获取或设置控件中被选中的RTF格式文本。如果把相应文本复制到另一个应用程序中，例如Word，该文本会保留所有的格式化信息
SelectedText | 与SelectedRtf一样，可以使用这个属性获取或设置被选中的文本。但与该属性的RTF版本不同，所有格式化的信息都会丢失
SelectionAlignment | 它表示选中文本的对齐方式，可以是Center、Left、Right
SelectionBullet | 使用这个属性可以确定选中的文本是否格式化为项目符号的格式，或使用它插入或删除项目符号
BulletIndent | 使用这个属性可以指定项目符号的缩进像素值
SelectionColor | 这个属性可以修改选中文本的颜色
SelectionFont | 这个属性可以修改选中文本的字体
SelectionLength | 使用这个属性可以设置或获取选中文本的长度
SelectionType | 这个属性包含了选中文本的信息。它可以确定是选择了一个或多个OLE对象，还是仅选择了文本
ShowSelectionMargin | 如果把这个属性设置为true，在RichTextBox的左边就会出现页边距，这将使用户更易选择文本
UndoActionName | 如果用户选择撤销某个动作，该属性将获取该操作的名称
SelectionProtected | 把这个属性设置为true，可以指定不修改文本的某些部分

从上表可以看出，大多数属性都与选中的文本有关。这是因为在用户处理其文本时，对它们应用的任何格式化操作都是对用户选择出来的文本进行的。万一没有选择文本，格式化操作就从文本的光标所在的位置开始应用，该位置称为插入点。

### 2、RichTextBox控件的事件

RichTextBox使用的大多数事件与TextBox使用的事件相同，下表列出了几个有趣的新事件。

名称 | 说明
:----:|:---:
LinkClicked | 在用户单击文本中的链接时，引发该事件
Protected | 在用户尝试修改已经标记为受保护的文本时，引发该事件
SelectionChanged | 在选中文本发生变化时，引发该事件。如果因某些原因不希望用户修改选中的文本，就可以在这里禁止修改

## 七、ListBox和CheckedListBox控件 ##

列表框用于显示一组字符串，可以一次从中选择一个或多个选项。与复选框和单选按钮一样，列表框也提供了要求用户选择一个或多个选项的方式。在设计期间，如果不知道用户要选择的数值个数，就应使用列表框（例如同事列表）。即使在设计期间知道所有可能的值，但列表中的值非常多，也应该考虑使用列表框。

ListBox类派生于ListControl类。后者提供了.NET Framework内置列表类型控件的基本功能。

另一类列表框称为CheckedListBox，派生于ListBox类。它提供的列表类似于ListBox，但除了文本字符串以外，每个列表选项还附带一个复选标记。

### 1、ListBox控件的属性

除非明确声明，下表中列出所有属性都可以用于ListBox类和CheckedListBox类。

属性 | 说明
:----:|:---:
SelectedIndex | 这个值表示列表框中选中项的基于0的索引，如果列表框可以一次选择多个选项，这个属性就包含选中列表中第一个选项的索引
ColumnWidth | 在包含多个列的列表框中，这个属性指定列宽
Items | Items集合包含列表框中的所有选项，使用这个集合的属性可以增加和删除选项
MultiColumn | 列表框可以有多个列。使用这个属性可以获取是否采用多列形式的信息，也可以设置是否采用多列形式
SelectedIndices | 这个属性是一个集合，包含列表框中选中项的所有基于0的索引
SelectedItem | 在只能选择一个选项的列表框中，这个属性包含选中的选项。在可以选择多个选项的列表框中，这个属性包含选中项中的第一项
SelectedItems | 这个属性是一个集合，包含当前选中的所有选项
SelectionMode | 在列表框中，可以使用ListSelectionMode枚举中的4种选择模式：None不能选择任何选项；One只能一次选择一个选项；MultiSimple可以选择多个选项。使用这个模式，在单击列表中的一项时，该项就会被选中，即使单击另一项，该项也保持选中状态，除非再次单击它；MultiExtended可以选择多个选项，用户还可以使用Ctrl、Shift和箭头键进行选择。它与MultiSimple不同，如果先单击一项，然后单击另一项，则只选中第二个单击的项
Sorted | 把这个属性设置为true。会使列表框对它包含的选项按字母顺序排列
Text | 许多控件都有Text属性。但这个Text属性与其他控件的Text属性大不相同。如果设置列表框控件的Text属性，它将搜索匹配该文本的选项，并选择该选项。如果获取text属性，返回的值是列表中第一个选中的选项。如果SelectionMode是None，就不能使用这个属性
CheckedIndices | 只适用于CheckedListBox，这个属性是一个集合，包含CheckedListBox中状态为checked或Indeterminate的所有选项的索引
CheckedItems | 只适用于CheckedListBox，这是一个集合，包含CheckedListBox中状态是checked或Indeterminate的所有选项
CheckOnClick | 只适用于CheckedListBox，如果这个属性是true，则选项就会在用户单击它时改变它的状态
ThreeDCheckBoxes | 只适用于CheckedListBox，设置这个属性，就可以选择平面或正常的CheckBoxes

### 2、ListBox控件的方法

为了高效地操作列表框，读者应了解它可以调用的一些方法。下表列出了最常用的方法。除非特别声明，否则这些方法均属于ListBox和CheckedListBox类。

方法 | 说明
:----:|:---:
ClearSelected() | 消除列表框中的所有选中项
FindString() | 查找列表框中第一个指定字符串开头的字符串，例如FindString("a")就是查找列表框中第一个以a开头的字符串
FindStringExact() | 与FindString类似，但必须匹配整个字符串
GetSelected() | 返回一个表示是否选择一个选项的值
SetSelected() | 设置或清除选项的选中状态
ToString() | 返回当前选中的选项
GetItemChecked() | 只适用于CheckedListBox，返回一个表示选项是否被选中的值
GetItemCheckState() | 只适用于CheckedListBox，返回一个表示选项的选中状态的值
SetItemChecked() | 只适用于CheckedListBox，设置指定为选中状态的选项
SetItemCheckState() | 只适用于CheckedListBox，设置选项的选中状态

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