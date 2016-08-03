# Cross2d

Cross platform simple 2D graphics library to create custom controls for Xamarin Forms.

Start by deriving a class from [Cross2DView](https://github.com/kpalosaa/cross2d/wiki/Documentation#cross2dview) 

```
public class YourClassName : Cross2DView
{
}
```

and implementing a function OnDraw

```
public class YourClassName : Cross2DView
{
    protected override void OnDraw(IContext context)
    {
    }
}
```
