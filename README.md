# README.md

[English](./README.en-US.md) | [简体中文](./README.zh-CN.md)

# Learn Notes

> [BlazorComponent/MASA.Blazor: Blazor component library based on Material Design. Support Blazor Server and Blazor WebAssembly. (github.com)](https://github.com/BlazorComponent/MASA.Blazor)
> 
> [BlazorComponent/BlazorComponent: There is no style blazor component library. (github.com)](https://github.com/BlazorComponent/BlazorComponent)

## Prepare

1. Fork 以上两个仓库
2. Git Clone {Fork后的 MASA.Blazor 项目}
3. cd 到 {MASA.Blazor\src} 目录
   1. Git Clone {Fork后的 BlazorComponent 项目}
4. 打开 MASA.Blazor.sln 解决方案

## Solution

| 目录        | 描述      |
| --------- | ------- |
| Test      | 单元测试    |
| Component | 组件      |
| CLI       | 生成文档的工具 |
| Doc       | 文档站点项目  |

# Projects

## BlazorComponent

    仅提供功能和接口。

## MASA.Blazor

    继承于 BlazorComponent 项目，为功能和接口提供样式。

## BlazorComponent.Web

    封装与 BlazorComponent 项目对应的 JS 模块。

    `JsInteropConstants` 包含了交互的常量信息。

## Structure

### Guide

    MASA.Blazor 项目内的 `M{Component}` 组件应继承于 `B{Component}` 组件。

    `B{Component}` 组件提供功能和接口，作为组件的基础。

    `M{Component}` 组件为 `B{Component}` 组件提供 MaterialDesign 风格的样式，作为组件的装饰，并可以被替代。

    `M{Component}` 组件使用 `SetComponentClass()` 方法设置组件各个部分的样式。

### BlazorComponent

- `IICon.cs`
  - 功能接口
- `BIcon.razor`
  - 设计组件的 DOM 结构
  - 使用 `@RenderPart(typeof(BFontIconSlot<>))` 渲染Slot插槽所在DOM结构的位置
- `BFontIconSlot<TIcon>.razor` where `TIcon : IIcon`
  - Slot插槽组件

### MASA.Balzor

- `MIcon.cs` : `BIcon, IICon`
  - 在 `SetComponentClass()` 方法中设置组件的 DOM 元素的样式
  - 在 `SetComponentClass()` 方法中调用 `AbstractProvider.Apply(typeof(BFontIconSlot<>), typeof(BFontIconSlot<MIcon>))` 设置插槽的真实组件类型

# Abstract

## BComponentBase

- `Queue<Func<Task>>` \_nextTickQueue
  
  - 类似 VUE ，逐帧渲染

- `IJSRuntime`
  
  - 封装与 JS 的互操作性

- `IErrorHandler`
  
  - 错误处理器

## BDomComponentBase : BComponentBase

- `CssProvider`
  
  - 定制样式

- `Watcher`
  
  - 类似 VUE ，监视属性变化并通知或执行对应委托
  
  - ```csharp
    protected override void OnInitialized() {
        base.OnInitialized();
        Watcher.Watch<int>(nameof(Age), (newValue, oldValue) => Console.WriteLine($"New Value: {newValue}, Old Value: {oldValue}");
    }
    ```
  
  - ```csharp
    public int Age {
        get => this.GetValue<int>();
        set => this.SetValue(value);
    }
    ```
  
  - 内部基于 `ObservableProperty<>` 实现属性变化时的通知功能

- `GetValue<>`
  
  - 使用 `[CallerMemberName]` 获取当前操作的属性的名称
  
  - 包装 `PropertyWatcher` 的读取方法

- `SetValue<>`
  
  - 使用 `[CallerMemberName]` 获取当前操作的属性的名称
  
  - 包装 `PropertyWatcher` 的写入方法

- `ComponentIdGenerator`
  
  - ID 生成器

## ComponentCssProvider

- 用于为组件设置样式

- ```csharp
  CssProvider
      .Apply(
          cssBuilder => cssBuilder.Add("").AddIf("", ()=> true),
          styleBuilder => styleBuilder.AddColor("").AddIf("", ()=> true))
      .Apply("header", cssBuilder => 
          cssBuilder.Add("").AddIf("", ()=> true))
      .Apply("content", cssBuilder => 
          cssBuilder.Add("").AddIf("", ()=> true))
      .Apply("border", cssBuilder => 
          cssBuilder.Add("").AddIf("", ()=> true))
  ```

## ComponentAbstractProvider

- 用于指明抽象或泛型组件的真实替代类型

- ```csharp
  AbstractProvider
      .Apply(typeof(BAlertWrapper<>), typeof(BAlertWrapper<MAlert>))
      .Apply(typeof(BButtonSlot<>), typeof(BButtonSlot<MAlert>))
      .Apply<BButton, MButton>("dismissible", attrs => {  attrs[nameof(MIcon.Dark)] = IsDarkTheme; });
  ```
