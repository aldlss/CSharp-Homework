# 记录一下

这波学到了许多，不得不记录一下。其实多亏了 new Bing 的帮助，很多知识点都是其告诉我的。

1.  MVVM 的编写方法。其实是写 ViewModel，但是之前一直不知道怎么搞，而且也想在 Xaml 里就写好，后来跟我说是在开头插入如下代码：
    ```xaml
    <Window.DataContext>
        <local:ViewModel/>
    </Window.DataContext>
    ```
    其中这个 local 是在上面有默认定义的 `xmlns:local="clr-namespace:homework4"`，应该大概是对应着一个命名空间这样。

    之前用 StaticResource 引入的方法还是有点蠢了。

2.  委托和事件妙用。把 Button 的 CanExecute 和 Execute 都以调用一个委托或者唤起一个事件的形式进行我觉得是十分高明的。  
    这样就可以像代码写的一样直接在 ViewModel 塞一个匿名函数进去了，这样的好处在于在 ViewModel 里可以比较方便地直接获取到各个控件。之前重构的原因之一也在于不同的构件散得太开了，获取不到，用 CommandParameter 的形式传输既不雅观，也不 MVVM. 坏处在于……ViewModel.cs 里的代码真的是一坨。

3.  Button 的 CanExecute 是在其 CanExecuteChanged 事件被调用后才会进行判断的。

    这个是我重构的主要原因，因为我是想在路径不合法的时候使合并按钮不可用，但是是在没办法在路径状态改变的时候调用 CanExecuteChanged 这个事件，它们不是一个控件，真没什么办法接触到。于是最后也是像目前的代码一样，加了一个路径变化时候的事件，然后在 ViewModel 里把文件合并的按钮的 OnCanExecuteChanged 直接做了一个函数塞进去了。

    其实这样都写在 ViewModel 里也有耦合度太高的弊病，但是我感觉既然都一个 ViewModel 了，耦合高点也是没办法的，而且写这个程序已经绰绰有余了。

4.  委托中使用异步。这个我也是服了，一开始查网上跟我说用 `event.BeginInvoke()` 这个语法，我学得有模有样的，感觉还好。结果一运行，跟我说已弃用用法，不给用了。这个时候才知道网上的教程都是什么古早的东西。  
    结果最后才发现大概直接 `await` 就行了，只能说放着新语法不用去学旧语法有点笑嘻了。

    使用异步委托主要是为了防止文件 IO 的时候太卡把 Execute 阻塞掉然后导致整个 UI 卡死（虽然不知道会不会这样），实际写下来感觉确实是比 Qt 的方便舒服很多。