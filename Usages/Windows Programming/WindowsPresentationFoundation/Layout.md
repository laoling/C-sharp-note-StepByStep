我们这节笔记记录WPF中关于布局的基础知识。

# 布局 #

在任意用户界面设计汇总，有一半的工作是以富有吸引力、灵活实用的方式组织内容。但真正的挑战是确保界面布局能够恰到好处地适应不同得窗口尺寸。

WPF用不同的容器（container）安排布局。每个容器有各自的布局逻辑——有些容器以堆栈方式布置元素，另一些容器在网格中不可见的单元格中排列元素，等等。在WPF中非常抵制基于坐标的布局，而是注重创建灵活的布局，使布局能够适应内部的内容的变化、不同的语言以及各种窗口尺寸。迁移到WPF的许多开发人员会觉得新布局系统令人倍感惊奇。

这里将介绍WPF布局模型的工作原理，并且将开始使用基本的布局容器。

## 一、理解WPF中的布局 ##

在Windows开发人员设计用户界面的方式上，WPF布局模型是一个重大改进。在WPF问世之前，Windows开发人员使用刻板的基于坐标的布局将控件放到正确位置。在WPF中，这种方式虽然也可行，但已经极少使用。大多数应用程序将使用类似于Web的flow布局；在使用流式布局模型时，控件可以扩大，并将其他控件挤到其他位置，开发人员能创建与显示分辨率和窗口大小无关的、在不同的显示器上正确缩放的用户界面；当窗口内容变化时，界面可调整自身，并且可以自如地处理语言的切换。要利用该系统的优势，首先需要进一步理解WPF布局模型的基本概念和假设。

#### 1.1 WPF布局原则

WPF窗口只能包含单个元素。为在WPF窗口中放置多个元素并创建更贴近实用的用户界面，需要在窗口上放置一个容器，然后在这个容器中添加其他元素。

在WPF中，布局由您使用的容器决定。尽管有多个容器可供选择，但理想的WPF窗口需要遵循下面几条重要原则：

* 不应显示设定元素的尺寸。元素应当可以改变尺寸以适合它们的内容。
* 不应使用屏幕坐标指定元素的位置。元素应当由它们的容器根据它们的尺寸、顺序以及其他特定于具体布局容器的信息进行排列。如果需要在元素之间添加空白空间，可使用Margin属性。
* 布局容器的子元素共享可用的空间。如果空间允许，布局容器会根据每个元素的内容尽可能为元素设置更合理的尺寸。它们还会向一个或多个子元素分配多余的空间。
* 可嵌套的布局容器。典型的用户界面使用Grid面板作为开始，Grid面板是WPF中功能最强大的容器，Grid面板可包含其他布局容器，包含的这些容器以更小的分组排列元素。

尽管对于这几条原则而言也有一些例外，但它们反映了WPF的总体设计目标。换句话说，如果创建WPF应用程序时遵循了这些原则，将会创建出更好的、更灵活的用户界面。如果不遵循这些原则，最终将得不到很适合WPF的并且难以维护的用户界面。

#### 1.2 布局过程

WPF布局包括两个阶段：测量阶段（measure）和排列阶段（arrange）。在测量阶段，容器遍历所有子元素，并询问子元素它们所期望的尺寸。在排列阶段，容器在合适的位置放置子元素。

当然，元素未必总能得到最合适的尺寸——有时容器没有足够大的空间以适应所含的元素。在这种情况下，容器为了适应可视化区域的尺寸，就必须剪裁不能满足要求的元素。在后面可看到，通常可通过设置最小窗口尺寸来避免这种情况。

#### 1.3 布局容器

所有WPF布局容器都是派生自System.Windows.Controls.Panel抽象类的面板。Panel类添加了少量成员，包括三个公有属性，下面列出了三个公有属性的详情：

名称 | 说明
:---:|:---:
Background | 该属性是用于为面板背景着色的画刷。如果想接收鼠标事件，就必须将该属性设置为非空值。
Children | 该属性是在面板中存储的条目集合。这是第一级条目——换句话说，这些条目自身也可以包含更多的条目。
IsItemsHost | 该属性是一个布尔值，如果面板用于显示与ItemsControl控件关联的项，该属性值为true。在大多数情况下，甚至不需要知道列表控件使用后台面板来管理它所包含的条目的布局。但如果希望创建自定义的列表，以不同方式放置子元素，该细节就变得很重要了。

就Panel基类本身而言没有什么特别的，但它是其他更多特性类的起点。WPF提供了大量可用于安排布局的继承自Panel的类，下表列出了其中几个最基本的类。与所有WPF控件和大多数可视化元素一样，这些类位于System.Windows.Controls名称空间中。

名称 | 说明
:---:|:---:
StackPanel | 在水平或垂直的堆栈中放置元素。这个布局容器通常用于更大、更复杂窗口中的一些小区域
WrapPanel | 在一系列可换行的行中放置元素。在水平方向上，WrapPanel面板从左向右放置条目，然后再随后的行中放置元素。在垂直方向上，WrapPanel面板在自上面下的列中放置元素，并使用附加的列放置剩余的条目
DockPanel | 根据容器的整个边界调整元素
Grid | 根据不可见的表格在行和列中排列元素，这是最灵活、最常见的容器之一
UniformGrid | 在不可见但是强制所有单元格具有相同尺寸的表中放置元素，这个布局容器不常用
Canvas | 使用固定坐标绝对定位元素。这个布局容器与传统Windows窗体应用程序最相似，但没有提供锚定或停靠功能，因此，对于尺寸可变的窗口，该布局容器不是合适的选择。如果选择的话，需要另外做一些工作

除这些核心容器外，还有几个更专业的面板，在各种控件中都可能遇到它们。这些容器包括专门用于包含特定控件子元素的面板——如TabPanel面板、ToolbarPanel面板以及ToolbarOverflowPanel面板。还有VirtualizingStackPanel面板，数据绑定列表控件使用该面板以大幅降低开销；还有InkCanvas控件，该控件和Canvas控件类似，但该控件支持处理平板电脑上的手写输入。

## 二、使用StackPanel面板进行简单布局 ##

StackPanel面板是最简单的布局容器之一。该面板简单地单行或单列中以堆栈形式放置其子元素。

例如包含四个按钮的窗口：

```xml
<Window x:Class="Layout.SimpleStack"
  xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
  xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
  Title="Layout" Height="223" Width="354">
  <StackPanel>
	<Label>A Button Stack</Label>
	<Button>Button 1</Button>
	<Button>Button 2</Button>
	<Button>Button 3</Button>
	<Button>Button 4</Button>
  </StackPanel>
</Window>
```

默认情况下，StackPanel面板按自上而下的顺序排列元素，使每个元素的高度适合它的内容。在这个示例中，这意味着标签和按钮的大小刚好足够适应它们内部包含的文本。所有元素都被拉伸到StackPanel面板的整个宽度，这也是窗口的宽度。如果加宽窗口，StackPanel面板也会变宽，并且按钮也会拉伸自身以适应变化。

通过设置Orientation属性，StackPanel面板也可用于水平排列元素：

```xml
<StackPanel Orientation="Horizontal">
```

现在元素指定它们的最小宽度并拉伸至容器面板的整个高度。根据窗口的当前大小，这可能导致一些元素不适应。

显然，这并未提供实际应用程序所需的灵活性。幸运的是，可使用布局属性对StackPanel面板和其他布局容器的工作方式进行精细调整，稍后会介绍。

#### 2.1 布局属性

尽管布局由容器决定，但子元素仍有一定的决定权。实际上，布局面板支持一小组布局属性，以便与子元素结合使用，下表列出了这些布局属性。

名称 | 说明
:---:|:---:
HorizontalAlignment | 当水平方向上有额外的空间时，该属性决定了子元素在布局容器中如何定位。可选用Center、Left、Right或Stretch等属性值
VerticalAlignment | 当垂直方向上有额外的空间时，该属性决定了子元素在布局容器中如何定位。可选用Center、Top、Buttom或Stretch等属性值
Margin | 该属性用于在元素的周围添加一定的空间。Margin属性是System.Windows.Thickness结构的一个实例。该结构具有分别用于为顶部、底部、左边和右边添加空间的独立组件
MinWidth和MinHeight | 这两个属性用于设置元素的最小尺寸。如果一个元素对于其他容器来说太大，该元素将被剪裁以适合容器
MaxWidth和MaxHeight | 这两个属性用于设置元素的最大尺寸。如果有更多可以使用的空间，那么在扩展子元素时就不会超出这一限制，即使将HorizontalAlignment和VerticalAlignment属性设置为Stretch也同样如此
Width和Height | 这两个属性用于显式的设置元素的尺寸。这一设置会重写为HorizontalAlignment和VerticalAlignment属性设置的Stretch值。但不能超出MinWidth、MinHight、MaxWidth和MaxHeight属性设置的范围

所有这些属性都是从FrameworkElement基类继承而来，所以在WPF窗口中可使用的所有图形小组件都支持这些属性。

这个属性列表就像它所没有包含的属性一样值得注意。如果查找熟悉的与位置相关的属性，例如top属性、right属性以及Location属性，是不会找到它们的。这是因为大多数布局容器都使用自动布局，并未提供显式定位元素的能力。

#### 2.2 对齐方式

为理解这些属性的工作原理，可进一步分析StackPanel面板的示例SimpleStack。

通常，对于Label控件，HorizontalAlignment属性的值默认为Left；对于Button控件，HorizontalAlignment属性的值默认为Stretch。这也是为什么每个按钮的宽度被调整为整列的宽度的原因所在。但可以改变这些细节：

```xml
<StackPanel>
   <Label HorizontalAlignment="Center">A Button Stack</Label>
   <Button HorizontalAlignment="Left">Button 1</Button>
   <Button HorizontalAlignment="Right">Button 2</Button>
   <Button>Button 3</Button>
   <Button>Button 4</Button>
</StackPanel>
```

这样，显式结果前面两个按钮的尺寸是它们应当具有的最小尺寸，并进行了对齐。而底部的两个按钮被拉伸至整个StackPanel面板的高度。如果改变窗口的尺寸，就会发现标签保持在中间位置，而前面两个按钮分别被粘贴到两边。

#### 2.3 边距

在SimpleStack示例中，当前情况下存在一个明显的问题。设计良好的窗口不只是包含元素——还应在元素之间包含一定的额外空间。为了添加额外的空间并使StackPanel面板中的按钮不那么紧密，可为控件设置边距。

当设置边距时，可为所有边设置相同的宽度，如下所示：

```xml
<Button Margin="5">Button 3</Button>
```

相应的，也可为控件的每个边以左、上、右、下的顺序设置不同的边距：

```xml
<Button Margin="5,10,5,10">Button 3</Button>
```

在代码中，使用Thickness结构来设置边距：

```csharp
cmd.Margin = new Thickness(5);
```

#### 2.4 最小尺寸、最大尺寸以及显示的设置尺寸

最后，每个元素都提供了Height和Width属性，用于显式地指定元素大小。但这种设置一般不是一个好主意。相反，如有必要，应当使用最大尺寸和最小尺寸属性，将控件限制在正确范围内。

例如，SimpleStack示例中你可能决定拉伸StackPanel容器中的按钮，使其适合StackPanel，但其宽度不能超过200单位，也不能小于100单位（默认情况下最初按钮的最小宽度是75单位）。下面是所需的标记：

```xml
<StackPanel Margin="3">
   <Label Margin="3" HorizontalAlignment="Center">A Button Stack</Label>
   <Button Margin="3" MaxWidth="200" MinWidth="100">Button 1</Button>
   <Button Margin="3" MaxWidth="200" MinWidth="100">Button 2</Button>
   <Button Margin="3" MaxWidth="200" MinWidth="100">Button 3</Button>
   <Button Margin="3" MaxWidth="200" MinWidth="100">Button 4</Button>
</StackPanel>
```

当StackPanel调整按钮的尺寸时，需要考虑以下几部分信息：

* **最小尺寸。**每个按钮的尺寸始终不能小于最小尺寸。
* **最大尺寸。**每个按钮的尺寸始终不能超过最大尺寸。
* **内容。**如果按钮中的内容需要更大的宽度，StackPanel容器会尝试扩展按钮。
* **容器尺寸。**如果最小宽度大于StackPanel面板的宽度，按钮的一部分将被剪裁掉。否则，不允许按钮比StackPanel面板更宽，即使不能适合按钮表面的所有文本也同样如此。
* **水平对齐方式。**因为默认情况下按钮的HorizontalAlignment属性值设置为Stretch，所有StackPanel将尝试放大按钮以占满StackPanel面板的整个宽度。

理解这个过程的关键在于，要认识到最小尺寸和最大尺寸设置了绝对界限。在这些界限内，StackPanel面板尝试反应按钮所期望的尺寸以及对齐方式的设置。

#### 2.5 Border控件

Border控件不是布局面板，而是非常便于使用的元素，经常与布局面板一起使用。所以，在继续介绍其他布局面板之前，我们先介绍一下Border控件是有意义的。

Border类非常简单。它只能包含一段嵌套内容，并为其添加背景或在其周围添加边框。为了深入地理解Border控件，只需要掌握下表中列出的属性就可以了。

名称 | 说明
:---:|:---
Background | 使用Brush对象设置边框中所有内容后面的背景。可使用固定颜色背景，也可使用其他更特殊的背景
BorderBrush和BorderThickness | 使用Brush对象设置位于Border对象边缘的边框的颜色，并设置边框的宽度。为显示边框，必须设置这两个属性
CornerRadius | 该属性可使边框具有雅致的圆角。CornerRadius的值越大，圆角效果就越明显
Padding | 该属性在边框和内部的内容之间添加空间（与此相对，Margin属性在边框之外添加空间）

下面是一个具有轻微圆角效果的简单边框，该边框位于一组按钮的周围，这组按钮包含在一个StackPanel面板中：

```xml
<Border Margin="5" Padding="5" Background="LightYellow" 
BorderBrush="SteelBlue" BorderThickness="3,5,3,5" CornerRadius="3" VerticalAlignment="Top">
  <StackPanel>
	<Button Margin="3">One</Button>
	<Button Margin="3">Two</Button>
	<Button Margin="3">Three</Button>
  </StackPanel>
</Border>
```

## 三、WrapPanel和DockPanel面板 ##

#### 3.1 WrapPanel面板

#### 3.2 DockPanel面板

#### 3.3 嵌套布局容器

## 四、Grid面板 ##

#### 4.1 调整行和列

#### 4.2 布局舍入

#### 4.3 跨越行和列

#### 4.4 分割窗口

#### 4.5 共享尺寸组

#### 4.6 UniformGrid面板

## 五、使用Canvas面板进行基于坐标的布局 ##

#### 5.1 Z顺序

#### 5.2 linkCanvas元素

## 六、布局实例 ##