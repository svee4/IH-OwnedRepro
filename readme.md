Expected
```
info: OwnedRepro.Worker[0]
      Publishing OwnedRepro.NoHandlerEvent
info: OwnedRepro.Worker[0]
      Publishing OwnedRepro.OneHandlerEvent
info: OwnedRepro.OneHandler.Handler[0]
      OneHandler called
info: OwnedRepro.Worker[0]
      Publishing OwnedRepro.TwoHandlerEvent
info: OwnedRepro.FirstTwoHandler.Handler[0]
      FirstTwoHandler called
info: OwnedRepro.SecondTwoHandler.Handler[0]
      SecondTwoHandler called
```

Actual:
```
info: OwnedRepro.Worker[0]
      Publishing OwnedRepro.NoHandlerEvent
fail: OwnedRepro.Worker[0]
      Exception when creating scope or calling handler OwnedRepro.Owned`1[Immediate.Handlers.Shared.IHandler`2[OwnedRepro.NoHandlerEvent,System.ValueTuple]]
      System.InvalidOperationException: No service for type 'Immediate.Handlers.Shared.IHandler`2[OwnedRepro.NoHandlerEvent,System.ValueTuple]' has been registered.
         at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService(IServiceProvider provider, Type serviceType)
         at Microsoft.Extensions.DependencyInjection.ServiceProviderServiceExtensions.GetRequiredService[T](IServiceProvider provider)
         at OwnedRepro.Owned`1.GetScope() in C:\dev\OwnedRepro\Owned.cs:line 8
         at OwnedRepro.Worker.<>c__DisplayClass5_1`1.<<Publish>b__0>d.MoveNext() in C:\dev\OwnedRepro\Worker.cs:line 41
info: OwnedRepro.Worker[0]
      Publishing OwnedRepro.OneHandlerEvent
info: OwnedRepro.OneHandler.Handler[0]
      OneHandler called
info: OwnedRepro.Worker[0]
      Publishing OwnedRepro.TwoHandlerEvent
info: OwnedRepro.SecondTwoHandler.Handler[0]
      SecondTwoHandler called
```