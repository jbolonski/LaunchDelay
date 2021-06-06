# Launch Delay

This will delay the launch of an application by time or based on another process to be started.

```
Usage: LaunchDelay [options] <Application>

Arguments:
  Application                          Application to Launch

Options:
  -?                                   Show help information.  
  -d|--delay <DELAY>                   Delay in seconds  
  -p|--process <PROCESS_NAME>          Process to wait for  
  -t|--timeout <PROCESS_WAIT_TIMEOUT>  Timeout if process doesn't launch after this in seconds
  ```
