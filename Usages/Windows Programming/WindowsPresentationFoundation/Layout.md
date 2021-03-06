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

显然，只使用StackPanel面板还不能帮助您创建出实用的用户界面。要设计出最终使用的用户界面，StackPanel面板还需要与其他更强大的布局容器协作。只有这样才能组装成完整的窗口。

最复杂的布局容器是Grid面板。在介绍Grid之前，有必要首先看一下WrapPanel和DockPanel面板，它们是WPF提供的两个更简单的布局容器。这两个布局容器通过不同的布局行为对StackPanel面板进行补充。

#### 3.1 WrapPanel面板

WrapPanel面板在可能的空间中，以一次一行或一列的方式布置控件。默认情况下，WrapPanel.Orientation属性设置为Horizontal；控件从左向右进行排列，再在下一行中排列。但可将WrapPanel.Orientation属性设置为Vertical，从而在多个列中放置元素。

提示：与StackPanel面板类似，WrapPanel面板实际上主要用来控制用户界面中一小部分的布局细节，并非用于控制整个窗口布局。例如可能使用WrapPanel面板以类似工具控件的方式将所有按钮保持在一起。

下面的示例中定义了一系列具有不同对齐方式的按钮，并将这些按钮放到一个WrapPanel面板中：

```xml
<WrapPanel Margin="3">
  <Button VerticalAlignment="Top">Top Button</Button>
  <Button MinHeight="60">Tall Button 2</Button>
  <Button VerticalAlignment="Bottom">Bottom Buttton</Button>
  <Button>Stretch Button</Button>
  <Button VerticalAlignment="Center">Centered Button</Button>
</WrapPanel>
```

正如这个示例，WrapPanel面板水平地创建了一系列假想的行，每一行的高度都被设置为所包含元素中最高元素的高度。其他控件能被拉伸以适应这一高度，或根据VerticalAlignment属性的设置进行对齐。因此，在该行中不关心各个按钮的VerticalAlignment属性的设置。

注意：WrapPanel面板是唯一一个不能通过灵活使用Grid面板代替的面板。

#### 3.2 DockPanel面板

DockPanel面板是更有趣的布局选项，它沿着一条外边缘来拉伸所包含的控件。理解该面板最简便的方法是，考虑一下位于许多Windows应用程序窗口顶部的工具栏。这些工具栏停靠到窗口顶部。与StackPanel面板类似，被停靠的元素选择它们布局的一个方面。

这里很明显的问题是：子元素如何选择停靠的边？

答案是通过Dock附加属性，可将该属性设置为Left、Right、Top或Bottom。放在DockPanel面板中的每个元素都会自动捕获该属性。

下面的示例在DockPanel面板的每条边上都停靠一个按钮：

```xml
<DockPanel LastChildFill="True">
  <Button DockPanel.Dock="Top">Top Button</Button>
  <Button DockPanel.Dock="Bottom">Bottom Button</Button>
  <Button DockPanel.Dock="Left">Left Button</Button>
  <Button DockPanel.Dock="Right">Right Button</Button>
  <Button>Remaining Space</Button>
</DockPanel>
```

该示例将DockPanel面板中的LastChildFill设置为true，该设置告诉DockPanel面板使用最后一个元素占满剩余空间。

显然，当停靠控件时，停靠顺序很重要。在这个示例中，顶部和底部按钮充满了DockPanel的整个边缘，这是因为这两个按钮首先被停靠。接着停靠左右按钮时，这两个按钮将位于顶部和底部之间。

可将多个元素停靠到同一边缘。这种情况下，元素按标记中声明的顺序停靠到边缘。而且，如果不喜欢空间分割或拉伸行为，可修改Margin属性、HorizontalAlignment属性以及VerticalAlignment属性，就像使用StackPanel面板进行布局时所介绍的那样。

#### 3.3 嵌套布局容器

很少单独使用StackPanel、WrapPanel、DockPanel面板。相反，它们通常用来设置一部分用户界面的布局。例如可使用DockPanel面板在窗口的合适区域放置不同的StackPanel和WrapPanel面板容器。

例如，假如希望创建一个标准对话框，在其右下角具有OK按钮和Cancel按钮，并且在窗口的剩余部分是一块较大的内容区域。在WPF中可采用几种方法完成这一布局，但最简单的方法如下，该方法使用前面介绍过的各种面板：

1）创建水平StackPanel面板，用于将OK按钮和Cancel按钮放在一起。
2）在DockPanel面板中放置StackPanel面板，将其停靠到窗口底部。
3）将DockPanel.ListChildFill属性设置为true，以使用窗口剩余的部分填充其他内容。在此可以添加另一个布局控件，或者只添加一个普通的TextBox控件。
4）设置边距属性，提供一定的空白空间。

下面是最终标记：

```xml
<DockPanel LastChildFill="True">
  <StackPanel DockPanel.Dock="Bottom" HorizontalAlignment="Right"
   Orientation="Horizontal">
	<Button Margin="10,10,2,10" Padding="3">OK</Button>
	<Button Margin="2,10,10,10" Padding="3">Cancel</Button>
  </StackPanel>
  <TextBox DockPanel.Dock="Top" Margin="10">This is a test.</TextBox>
</DockPanel>
```

在这个示例中，Padding属性在按钮边框与内部的内容之间添加了尽量少的空间。

乍一看，相对于使用坐标精确放置控件而言，这有些多余。在很多情况下确实如此。不过，设置时间固然较长，但这样做的好处是在将来可以很方便地修改用户界面。

与Windows旧式的用户界面框架相比，这里使用的标记更整洁、更简单也更紧凑。如果为这个窗口添加一些样式，还可对该窗口进行进一步改进，并移除不必要的细节，从而创建真正的自适应用户界面。

## 四、Grid面板 ##

Grid面板是WPF中功能最强大的布局容器。很多使用其他布局控件能完成的功能，用Grid面板也能实现。Grid面板也是将窗口分割成更小区域的理想工具。

实际上，由于Grid面板十分有用，因此在VS中为窗口添加新的XAML文档时，会自动添加Grid标签作为顶级容器，并嵌套在Window根元素中。

Grid面板将元素分隔到不可见的行列网格中。尽管可在一个单元格中放置多个元素，但在每个单元格中只放置一个元素通常更合理。当然，在Grid单元格中的元素本身也可能是另一个容器，该容器组织它所包含的一组控件。

提示：方便调试，我们可将不可见的Grid面板，通过将Grid.ShowGridLines属性设置为true，从而更清晰地观察Grid面板。这一特性并不是真正试图美化窗口，反而是为了方便调试，设计该特性旨在帮助理解Grid面板如何将其自身分割成多个小区域。

需要两个步骤来创建基于Grid面板的布局。首先，选择希望使用的行和列的数量。然后，为每个包含的元素指定恰当的行和列，从而在合适的位置放置元素。

Grid面板通过使用对象填充Grid.ColumnDefinitions和Grid.RowDefinitions集合来创建网格和行。例如如果确定需要两行和三行，可添加以下标签：

```xml
<Grid ShowGridLines="True">
  <Grid.RowDefinitions>
	<RowDefinition></RowDefinition>
	<RowDefinition></RowDefinition>
  </Grid.RowDefinitions>
  <Grid.ColumnDefinitions>
	<ColumnDefinition></ColumnDefinition>
	<ColumnDefinition></ColumnDefinition>
	<ColumnDefinition></ColumnDefinition>
  </Grid.ColumnDefinitions>

  ...
</Grid>
```

正如本例演示的，在RowDefinition和ColumnDefinition元素中不必提供任何信息。如果保持他们为空，Grid面板将在所有行和列之间平均分配空间。在本例中，每个单元格的尺寸完全相同，具体取决于包含窗口的尺寸。

为在单元格中放置各个元素，需要使用Row和Column附加属性。这两个属性的值都是从0开始的索引数。例如以下标记演示了如何创建Grid面板，并使用按钮填充Grid面板的部分单元格。

```xml
<Grid ShowGridLines="True">
...
  <Button Grid.Row="0" Grid.Column="0">Top Left</Button>
  <Button Grid.Row="0" Grid.Column="1">Middle Left</Button>
  <Button Grid.Row="1" Grid.Column="2">Bottom Right</Button>
  <Button Grid.Row="1" Grid.Column="1">Bottom Middle</Button>
</Grid>
```

每个元素必须被明确地放在对应的单元格中。可在单元格中放置多个元素，或让单元格保持为空。也可以不按顺序声明元素，正如本例中的最后两个按钮那样。但如果逐行定义控件，可使标记更清晰。

此处存在例外情况。如果不指定Grid.Row属性，Grid面板会假定该属性的值为0。对于Grid.Column属性也是如此。因此，在Grid面板的第一个单元格中放置元素时可不指定这两个属性。

#### 4.1 调整行和列

如果Grid面板只是按照比例分配尺寸的行和列的集合，它也就没什么用处了。幸运的是，情况并非如此。为了充分发挥Grid面板的潜能，可更改每一行和每一列的尺寸设置方式。

Grid面板支持以下三种设置尺寸的方式：

* 绝对设置尺寸方式。设置设备无关单位准确地设置尺寸。这是最无用的策略，因为这种策略不够灵活，难以适应内容大小和容器大小的改变，而且更难以处理本地化。
* 自动设置尺寸方式。每行和每列的尺寸刚好满足需要。这是最有用的尺寸设置方式。
* 按比例设置尺寸方式。按比例将空间分割到一组行和列中。这是对所有行和列的标准设置。

为了获得最大的灵活性，可混合使用这三种尺寸设置方式。例如创建几个自动设置尺寸的行，然后通过按比例设置尺寸的方式让最后一行或两行充满剩余的空间，这通常是很有用的。

可通过将ColumnDefinition对象的Width属性或RowDefinition对象的Height属性设置为数值来确定尺寸的设置方式。例如下面代码显示了如何设置100设备无关单位的绝对宽度：

```xml
<ColumnDefinition Width="100"></ColumnDefinition>
```

为使用自动尺寸设置方式，可使用Auto值：

```xml
<ColumnDefinition Width="Auto"></ColumnDefinition>
```

为了使用按比例尺寸设置方式，需要使用星号（*）：

```xml
<ColumnDefinition Width="*"></ColumnDefinition>
```

如果混合使用按比例尺寸设置方式和其他尺寸设置方式，就可以在剩余的任意空间按比例改变行或列的尺寸。

如果希望不均匀的分割剩余空间，可指定权重，权重必须放在星号之前。例如如果有两行是按比例设置尺寸，并希望第一行的高度是第二行高度的一半，那么可以使用如下设置来分配剩余空间：

```xml
<RowDefinition Height="*"></RowDefinition>
<RowDefinition Height="2*"></RowDefinition>
```

上面的代码告诉Grid面板，第二行的高度应是第一行高度的二倍。可使用您喜欢的任何数字来分配剩下的空间。

使用这些尺寸设置方式，我们可以重现前面的示例对话框。使用顶级的Grid容器而不是使用DockPanel面板。下面是所需要的标记：

```xml
<Grid ShowGridLines="True">
  <Grid.RowDefinition>
	<RowDefinition Height="*"></RowDefinition>
	<RowDefinition Height="Auto"></RowDefinition>
  </Grid.RowDefinition>
  <TextBox Margin="10" Grid.Row="0">This is a test.</TextBox>
  <StackPanel Grid.Row="1" HorizontalAlignment="Right" Orientation="Horizontal">
	<Button Margin="10,10,2,10" Padding="3">OK</Button>
	<Button Margin="2,10,10,10" Padding="3">Cancel</Button>	
  </StackPanel>
</Grid>
```

#### 4.2 布局舍入

WPF使用分辨率无关的测量系统。尽管该测量系统为使用各种不同的硬件提供了灵活性，但有时也会引入一些问题。其中一个问题是元素可能被对齐到子像素边界——换句话说，使用没有和物理像素准确对齐的小数坐标定位元素。可通过为相邻的布局容器提供非整数尺寸强制发生这个问题。但当不希望发生这个问题时，在某些情况下该问题也可能出现，例如当创建按比例设置尺寸的Grid面板时就可能会发生该问题。

WPF在处理边界像素不为整数时，对微小的错位会使用反锯齿功能混合原本清晰的像素边界边缘。由于没有使用布局舍入（layout rounding），所以矩形的清晰边缘在特定的窗口尺寸下变得模糊了。

如果这个问题影响到布局，可以采用一种方法很方便地解决该问题。只需要将布局容器的UseLayoutRounding属性设置为true：

```xml
<Grid UseLayoutRounding="True">
```

现在，WPF会确保布局容器中的所有内容对齐到最近的像素边界，从而消除了所有模糊问题。

#### 4.3 跨越行和列

您已经看到如何使用Row和Column附加属性在单元格中放置元素。还可以使用另外两个附加属性使元素跨越多个单元格，这两个附加属性是RowSpan和ColumnSpan。这两个属性使用元素将会占用的行数和列数进行设置。

例如，下面的按钮将占据第一行中的第一个和第二个单元格的所有空间：

```xml
<Button Grid.Row="0" Grid.Column="0" Grid.RowSpan="2">Span Button</Button>
```

下面的代码通过跨越两列和两行，拉伸按钮使其占据所有4个单元格：

```xml
<Button Grid.Row="0" Grid.Column="0" Grid.RowSpan="2" Grid.ColumnSpan="2">Span Button</Button>
```

通过跨越行和列可得到更有趣的效果，当需要在由分割器或更长的内容区域分开的表格结构中放置元素时，这是非常方便的。

使用列跨越特征，可以只使用Grid面板重新编写前面的简单对话框示例。Grid面板将窗口分割成三列，展开文本框使其占据所有三列，并使用最后两列对齐OK按钮和Cancel按钮：

```xml
<Grid ShowGridLines="True">
  <Grid.RowDefinition>
	<RowDefinition Height="*"></RowDefinition>
	<RowDefinition Height="Auto"></RowDefinition>
  </Grid.RowDefinition>
  <Grid.ColumnDefinition>
	<ColumnDefinition Width="*"></RowDefinition>
	<ColumnDefinition Width="Auto"></RowDefinition>
	<ColumnDefinition Width="Auto"></RowDefinition>
  </Grid.ColumnDefinition>
  <TextBox Margin="10" Grid.Row="0" Grid.Column="0" Grid.ColumnSpan="3">This is a test.</TextBox>  
  <Button Margin="10,10,2,10" Padding="3" Grid.Row="1" Grid.Column="1">OK</Button>
  <Button Margin="2,10,10,10" Padding="3" Grid.Row="1" Grid.Column="2">Cancel</Button>	
</Grid>
```

大多数开发人员认为这种布局不清晰也不明智。列宽由窗口底部的两个按钮尺寸决定，这使得难以向已经存在的Grid结构中添加新内容。即使向这个窗口增加很少的内容，也必须创建新的列集合。

正如上面显示的，当为窗口选择布局容器时，不仅关心能否得到正确的布局行为——还希望构建便于在未来维护和增强的布局结构。

一条正确的经验法则是，对于一次性的布局任务，例如排列一组按钮，使用最小的布局容器。但如果需要为窗口中的多个区域使用一致的结构，对于标准化布局而言，Grid面板是必不可少的工具。

#### 4.4 分割窗口

每个Windows用户都见过分割条——能将窗口的一部分与另一部分分离的可拖动分割器。例如，当使用Windows资源管理器时，会看到一系列文件夹和一系列文件。可拖动它们之间的分割条来确定每部分占据窗口的比例。

在WPF中，分隔条由GridSplitter类表示，它是Grid面板的功能之一。通过为Grid面板添加GridSplitter对象，用户就可以改变行和列的尺寸。

大多数开发人员认为WPF中GridSplitter类不是最直观的。理解如何使用GridSplitter类，从而得到期望的效果需要一定的经验。下面列出几条指导原则：

* GridSplitter对象必须放在Grid单元格中。可与已经存在的内容一并放到单元格中，这时需要调整边距设置，使它们不相互重叠。更好的方法是预留一列或一行专门用于放置GridSplitter对象，并将预留行或列的Height或Width属性的值设置为Auto。
* GridSplitter对象总是改变整行或整列的尺寸。为使GridSplitter对象的外观和行为保持一致，需要拉伸GridSplitter对象使其穿越整行或整列，而不是将其限制在单元格中。为此，可使用前面介绍的RowSpan或ColumnSpan属性。
* 最初GridSplitter对象很小不易看到。为了使其更可用，需要为其设置最小尺寸。对于竖直分割条，需要将VerticalAlignment属性设置为Stretch，并将Width设置为固定值。对于水平分割条，需要设置HorizontalAlignment属性来拉伸，并将Height属性设置为固定值。
* GridSplitter对齐方式还决定了分割条是水平的还是竖直的。对于水平分割条，需要将VerticalAlignment属性设置为Center（默认值），以指明拖动分割条改变上面行和下面行的尺寸。对于竖直分割条，需要将HorizontalAlignment属性设置为Center，以改变分割条两侧列的尺寸。

下面我们写一个程序列出GridSplitter对象的细节：

```xml
<Grid>
  <Grid.RowDefinitions>
	<RowDefinition></RowDefinition>
	<RowDefinition></RowDefinition>
  </Grid.RowDefinitions>
  <Grid.ColumnDefinitions>
	<ColumnDefinition MinWidth="100"></ColumnDefinition>
	<ColumnDefinition Width="Auto"></ColumnDefinition>
	<ColumnDefinition MinWidth="50"></ColumnDefinition>
  </Grid.ColumnDefinitions>

  <Button Grid.Row="0" Grid.Column="0" Margin="3">Left</Button>
  <Button Grid.Row="0" Grid.Column="2" Margin="3">Right</Button>
  <Button Grid.Row="1" Grid.Column="0" Margin="3">Left</Button>
  <Button Grid.Row="1" Grid.Column="2" Margin="3">Right</Button>

  <GridSplitter Grid.Row="0" Grid.Column="1" Grid.RowSpan="2"
   Width="3" VerticalAlignment="Stretch" HorizontalAlignment="Center"
   ShowsPreview="False"></GridSplitter>
</Grid>
```

此代码还包含了一处细节，在声明GridSplitter对象时，将ShowsPreview属性设置为false。因此，当把分割条从一边拖到另一边时，会立即改变列的尺寸。但是如果将此属性设置为true，当拖动分割条时就会看到一个灰色阴影跟随着鼠标指针，用于显示将在何处进行分割。并且直到释放了鼠标键之后的尺寸才改变。如果GridSplitter对象获得了焦点，也可以使用箭头键改变相应的尺寸。

如果希望更大幅度的移动分割条，可以调整DragIncrement属性。如果希望控制列的最大尺寸和最小尺寸，只需要在ColumnDefinitions部分设置合适的属性。

#### 4.5 共享尺寸组

正如前面看到的，Grid面板包含一个行列集合，可以明确的按比例确定行和列的尺寸，或根据其子元素的尺寸确定行和列的尺寸。还有另一种确定一行或一列的尺寸的方法——与其他行或列相匹配。这是通过成为共享尺寸组的特性实现的。（shared size groups）

共享尺寸组的目标是保持用户界面独立部分的一致性。例如可能希望改变一列的尺寸以适应其内容，并改变另一列使其与前面一列改变后的尺寸相匹配。然而，共享尺寸组的真正优点是使独立的Grid控件具有相同的比例。

共享的列可用于不同的网格中。同样，共享的列可占据不同的位置，从而可以在一个Grid面板中的其中一列和另一个Grid面板中另一列之间创建一种联系。显然，这些列可包含完全不同的内容。

当使用共享尺寸组时，就像创建了一列（行）的定义，列（行）定义在多个地方被重复使用。这不是简单的将一列（行）复制到另外一个地方。

甚至可为其中一个Grid对象添加GridSplitter。当用户改变一个Grid面板中列的尺寸时，另一个Grid面板中的共享列会同时相应的改变尺寸。

可轻而易举的创建共享组。只需要使用对应的字符串设置两列的SharedSizeGroup属性即可。

下面这个示例中，两列都使用了名为TextLabel的分组：

```xml
<Grid Margin="3" Background="LightYellow" ShowGridLines="True">
  <Grid.ColumnDefinitions>
	<ColumnDefinition Width="Auto" SharedSizeGroup="TextLabel"></ColumnDefinition>
	<ColumnDefinition Width="Auto"></ColumnDefinition>
	<ColumnDefinition></ColumnDefinition>
  </Grid.ColumnDefinitions>

  <Label Margin="5">A very long bit of text</Label>
  <Label Grid.Column="1" Margin="5">More text</Label>
  <TextBox Grid.Column="2" Margin="5">A text box</TextBox>
</Grid>
...
<Grid Margin="3" Background="LightYellow" ShowGridLines="True">
  <Grid.ColumnDefinitions>
	<ColumnDefinition Width="Auto" SharedSizeGroup="TextLabel"></ColumnDefinition>
	<ColumnDefinition></ColumnDefinition>
  </Grid.ColumnDefinitions>

  <Label Margin="5">Short</Label>
  <TextBox Grid.Column="1" Margin="5">A text box</TextBox>
</Grid>
```

还有一个细节，对于整个应用程序来说，共享尺寸组并不是全局的，因为多个窗口可能在无意间使用相同名称。可以假定共享尺寸组被限制在当前窗口，但是WPF更加严格。

为了共享一个组，需要在包含具有共享列的Grid对象的容器中，在包含Grid对象之前明确地将Grid.IsSharedSizeScope附加属性设置为true。当然可以使用Grid嵌套，也可以使用不同的容器，如DockPanel或StackPanel。

#### 4.6 UniformGrid面板

有一种网格不遵循前面介绍的所有原则——UniformGrid面板。与Grid面板不同，UniformGrid面板不需要预先定义的行和列。相反，通过简单的设置Rows和Columns属性来设置其尺寸。

每个单元格始终具有相同的大小，因为可用的空间被均分。最后，元素根据定义的顺序被放置到适当的单元格中。UniformGrid面板中没有Row和Column附加属性，也没有空白单元格。

下面举个简单的示例，该示例使用4个按钮填充UniformGrid面板：

```xml
<UniformGrid Rows="2" Column="2">
  <Button>Top Left</Button>
  <Button>Top Right</Button>
  <Button>Bottom Left</Button>
  <Button>Bottom Right</Button>
</UniformGrid>
```

与Grid面板相比，UniformGrid面板很少使用。Grid面板是用于创建简单乃至复杂窗口布局的通用工具。UniformGrid面板是一种更特殊的布局容器，主要用于在刻板的网格中快速地布局元素。

## 五、使用Canvas面板进行基于坐标的布局 ##

到目前为止唯一尚未介绍的布局容器是Canvas面板。Canvas面板允许使用精确的坐标放置元素，如果设计数据驱动的富窗体和标准对话框，这并非好的选择；但如果需要构建其他一些不同的内容（为图形工具创建绘图表面），Canvas面板可能是个有用的工具。

Canvas面板还是最轻量级的布局容器。这是因为Canvas面板没有包含任何复杂的布局逻辑，用以改变其子元素的首选尺寸。Canvas面板只是在指定的位置放置其子元素，并且其子元素具有所希望的精确尺寸。

为在Canvas面板中定位元素，需要设置Canvas.Left和Canvas.Top附加属性。Canvas.Left属性设置元素左边和Canvas面板左边之间的单位数，Canvas.Top属性设置子元素顶边和Canvas面板顶边之间的单位数。同样，这些数值也是以设备无关单位设置的，当将系统DPI设置为96dpi时，设备无关单位恰好等于通常的像素。

可使用Width和Height属性明确设置子元素的尺寸。与使用其他面板相比，使用Canvas面板时这种设置更普遍，因为Canvas面板没有自己的布局逻辑（并且当需要精确控制组合元素如何排列时，经常会使用Canvas面板）。如果没有设置Width和Height属性，元素会获取它所期望的尺寸——换句话说，它将变得足够大以适应其内容。

下面是个包含了4个按钮的简单Canvas面板示例：

```xml
<Canvas>
  <Button Canvas.Left="10" Canvas.Top="10">(10,10)</Button>
  <Button Canvas.Left="120" Canvas.Top="30">(120,30)</Button>
  <Button Canvas.Left="60" Canvas.Top="80" Width="50" Height="50">(60,80)</Button>
  <Button Canvas.Left="70" Canvas.Top="120" Width="100" Height="50">(70,120)</Button>
</Canvas>
```

如果改变窗口大小，Canvas面板就会拉伸以填满可用空间，但Canvas面板上的控件不会改变其尺寸和位置。Canvas面板不包含任何锚定和停靠功能，这两个功能是在Windows窗体中使用坐标布局提供的。造成该问题的部分原因是为了防止以不当目的使用Canvas面板。

与其他容器一样，可在用户界面总嵌套Canvas面板。这意味着可使用Canvas面板在窗口的一部分绘制一些细节内容，而在窗口的其余部分使用更合乎标准的WPF面板。

#### 5.1 Z顺序

如果Canvas面板中有多个互相重叠的元素，可通过设置Canvas.ZIndex附加属性来控制他们的层叠方式。

添加的所有元素通常都具有相同的ZIndex值——0.如果元素具有相同的ZIndex值，就按它们在Canvas.Children集合中的顺序进行显示，这个顺序依赖于元素在XAML标记中定义的顺序。在标记中靠后位置声明的元素（如按钮（70，120））会显示在前面声明的元素（如按钮（120，30））的上面。

然而，可通过增加任何子元素的ZIndex值来提高层次级别。因为具有更高ZIndex值的元素始终显示在较低ZIndex值的元素的上面。使用这一技术，可改变前一示例中的分层情况：

```xml
  <Button Canvas.Left="60" Canvas.Top="80" Canvas.ZIndex="1" Width="50" Height="50">(60,80)</Button>
  <Button Canvas.Left="70" Canvas.Top="120" Width="100" Height="50">(70,120)</Button>
```

注意：应用于Canvas.ZIndex属性的实际值并无意义。重要细节的是一个元素的ZIndex值和另一个元素的ZIndex值相比较如何。可将ZIndex属性设置为任何正整数或负整数。

如果需要通过代码来改变元素的位置，ZIndex属性是非常有用的。只需要调用Canvas.SetZIndex()方法，并传递希望修改的元素和希望使用的新ZIndex值即可。遗憾的是，并不存在BringToFront()或SendToBack()方法——要实现这一行为，需要跟踪最高和最低的ZIndex值。

#### 5.2 InkCanvas元素

WPF还提供了InkCanvas元素，它与Canvas面板在某些方面是类似的。和Canvas面板一样，InkCanvas元素定义了4个附加属性（Top Left Bottom和Right），可将这4个附加属性应用于子元素，以根据坐标进行定位。然而，基本的内容区别很大——实际上，InkCanvas类不是派生自Canvas类，甚至也不是派生自Panel基类，而是直接派生自FrameworkElement类。

InkCanvas元素的主要目的是用于接收手写输入。手写笔是一种在平板PC上使用的类似于钢笔的输入设备，然而InkCanvas同时也可以使用鼠标进行工作，就像使用手写笔一样。因此，用户也可以使用鼠标在InkCanvas元素上绘制线条，或者选择以及操作InkCanvas中的元素。

InkCanvas元素实际上包含两个子内容集合。一个是Children集合，它保存任意元素，就像Canvas面板一样。每个子元素可根据Top、Left、Bottom、Right属性进行定位。另一个是Strokes集合，它保存System.Windows.Ink.Stroke对象，该对象表示用户在InkCanvas元素上绘制的图形输入。用户绘制的每条直线或曲线都变成独立的Stroke对象。

比如一个关于给照片进行标注的示例：

```xml
<InkCanvas Name="inkCanvas" Background="LightYellow" EditingMode="Ink">
  <Image Source="office.jpg" InkCanvas.Top="10" InkCanvas.Left="10" 
   Width="287" Height="319"></Image>
</InkCanvas>
```

根据为InkCanvas.EditingMode属性设置的值，可以采用截然不同的方式使用InkCanvas元素，下面列出了所有选项。

名称 | 说明
:---:|:---
Ink | InkCanvas元素允许用户绘制批注，这是默认模式，当用户用鼠标或手写笔绘图时，会绘制画笔
GestureOnly | InkCanvas元素不允许用户绘制笔画批注，但会关注预先定义的特定姿势。能识别的姿势的完整列表由System.Windows.Ink.ApplicationGesture枚举给出
InkAndGesture | InkCanvas元素允许用户绘制笔画批注，也可以识别预先定义的姿势
EraseByStroke | 当单击笔画时，InkCanvas元素会擦除笔画。如果用户使用手写笔，可使用手写笔的底端切换到该模式
EraseByPoint | 当单击笔画时，InkCanvas元素会擦除笔画中被单击的部分（笔画上的一个点）
Select | InkCanvas面板允许用户选择保存在Children集合中的元素，要选择一个元素，用户必须单击该元素或拖动“套索”选择该元素。一旦选择一个元素，就可以移动该元素、改变其尺寸或将其删除
None | InkCanvas元素忽略鼠标和手写笔输入

InkCanvas元素会引发多种事件，当编辑模式改变时会引发ActiveEditingModeChanged事件，在GestureOnly或InkAndGesture模式下删除姿势时会引发Gesture事件，绘制完笔画时会引发StrokeCollected事件，擦除笔画时会引发StrokeErasing事件和StrokeErased事件，在Select模式下选择元素或改变元素时会引发SelectionChanging事件、SelectionChanged事件、SelectionMoving事件、SelectionMoved事件、SelectionResizing事件和SelectionResized事件。其中名称以ing结尾的事件表示动作将要发生，但可以通过设置EventArgs对象的Cancel属性取消事件。

在Select模式下，InkCanvas元素可为拖动以及操作内容提供功能强大的设计界面。显然Select模式很有趣，但并不适合于构建绘图工具。

## 六、布局实例 ##

* 列设置
* 动态内容
* 组合式用户界面

具体内容实践会进行详细操作。
