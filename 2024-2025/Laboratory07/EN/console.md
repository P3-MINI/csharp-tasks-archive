# Lab07:

## Expected Results:

```
STAGE01:

Server00 [192.168.1.10]
[192.168.1.10]: Status => Stopped
[192.168.1.10]: Load => 0

STAGE02:

Added Server00 [192.168.1.10]
[192.168.1.10]: Status => Running
[192.168.1.10]: Load => 41
Added Server01 [192.168.1.20]
[192.168.1.20]: Status => Running
[192.168.1.20]: Load => 48
Added Server02 [192.168.1.30]
[192.168.1.30]: Status => Running
[192.168.1.30]: Load => 45
Added Server03 [192.168.1.40]
[192.168.1.40]: Status => Running
[192.168.1.40]: Load => 46

Removed Server03 [192.168.1.40]
Couldn't remove server Server03 [192.168.1.40] (it has already been removed).

STAGE03:

Server00 [192.168.1.10] retrieved successfully!

Admin has added new traffic redirection policy.
Server00 [192.168.1.10] is getting overloaded...
[192.168.1.10]: Load => 99
[192.168.1.10]: Load => 50
Redirecting load (49) to... Server01 [192.168.1.20]
[192.168.1.20]: Load => 97
[192.168.1.20]: Load => 50
Redirecting load (47) to... Server02 [192.168.1.30]
[192.168.1.30]: Load => 92
[192.168.1.30]: Status => Failed
Server02 [192.168.1.30]: Internal Server Error!

Adding new servers to the system:
Added Server05 [192.168.1.60]
[192.168.1.60]: Status => Running
[192.168.1.60]: Load => 40
Added Server06 [192.168.1.70]
[192.168.1.70]: Status => Running
[192.168.1.70]: Load => 40
Added Server07 [192.168.1.80]
[192.168.1.80]: Status => Running
[192.168.1.80]: Load => 44

Getting the maintenance order...
Server02 [192.168.1.30] -> (Failed, 92)
Server00 [192.168.1.10] -> (Running, 50)
Server01 [192.168.1.20] -> (Running, 50)
Server07 [192.168.1.80] -> (Running, 44)
Server05 [192.168.1.60] -> (Running, 40)
Server06 [192.168.1.70] -> (Running, 40)
```
