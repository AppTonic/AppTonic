[![Build status](https://ci.appveyor.com/api/projects/status/62g8m51g3491so4f)](https://ci.appveyor.com/project/CraigSmitham/appfunc)
[![NuGet Package](http://img.shields.io/nuget/v/AppFunc.svg?style=flat)](https://www.nuget.org/packages/AppFunc)

# AppFunc
AppFunc provides a message-based mediator interface in the form of an in-process request dispatcher.

```
PM> Install-Package AppFunc
```


## Getting Started / Documentation

Check out the [Quick Start Guide](http://appfunc.github.io/quick-start/) and reference the wiki for [documentation](https://github.com/AppFunc/AppFunc/wiki).

[Example projects](https://github.com/AppFunc/AppFunc/tree/master/src) are included in the repository to help you get started.



## IoC Integration
Using the `AppFunc.CommonServiceLocator` NuGet package, AppFunc integrates with your desired IoC container
to route request messages to the appropriate application message handler. Alternatively (or in combination), AppFunc
allows you to register application request handlers just lambda expressions - enabling the use of partial application
for application composition instead of using a IoC container.

```
PM> Install-Package AppFunc.CommonServiceLocator
```


## License
Apache 2.0

Copyright 2014 Craig Smitham