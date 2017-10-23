# LangExtAsyncOptFail
Sample solution showing issue with Language-Ext package and failing async operation

## Steps

1. Restore packages
1. Run from Visual Studio or command line
1. Press a key to actually start processing

See generated exception, the one wrapped in an *AggregateException* due to `.Wait()`:

**Type:**  
> LanguageExt.BottomException

**Message:**  
> Value is in a bottom state and therefore not valid.  This can happen when the value was filtered and the predicate returned false and there was no valid state the value could be in.  If you are going to use the type in a filter you should check if the IsBottom flag is set before use.  This can also happen if the struct wasn't initialised properly and then used.

**Stacktrace:**  
>    at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()  
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)  
   at TaskExtensions.<SelectMany>d__5`3.MoveNext()

