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
[192.168.1.10]: Load => 7
Added Server01 [192.168.1.20]
[192.168.1.20]: Status => Running
[192.168.1.20]: Load => 42
Added Server02 [192.168.1.30]
[192.168.1.30]: Status => Running
[192.168.1.30]: Load => 25
Added Server03 [192.168.1.40]
[192.168.1.40]: Status => Running
[192.168.1.40]: Load => 34

Removed Server03 [192.168.1.40]
Couldn't remove server Server03 [192.168.1.40] (it has already been removed).

STAGE03:

Admin has added a new rule (Rule75)!
Server00 [192.168.1.10] is getting overloaded...
[192.168.1.10]: Load => 55
[192.168.1.10]: Load => 70
[192.168.1.10]: Load => 75
[192.168.1.10]: Load => 80
Executing Rule75... on Server00 [192.168.1.10]
[192.168.1.10]: Status => OverLoaded
System reacted to Server00 [192.168.1.10] overloading correctly!
[192.168.1.10]: Load => 90

Adding new servers to the system:
Added Server05 [192.168.1.60]
[192.168.1.60]: Status => Running
[192.168.1.60]: Load => 1
Added Server06 [192.168.1.70]
[192.168.1.70]: Status => Running
[192.168.1.70]: Load => 3
Added Server07 [192.168.1.80]
[192.168.1.80]: Status => Running
[192.168.1.80]: Load => 20
[192.168.1.60]: Status => Failed
[192.168.1.70]: Status => Failed
[192.168.1.80]: Status => Stopped

Performing clustering...
Cluster with status [OverLoaded]:
Server00 [192.168.1.10]

Cluster with status [Running]:
Server01 [192.168.1.20]
Server02 [192.168.1.30]

Cluster with status [Failed]:
Server05 [192.168.1.60]
Server06 [192.168.1.70]

Cluster with status [Stopped]:
Server07 [192.168.1.80]

Shutting down the system...
[192.168.1.10]: Status => Stopped
[192.168.1.10]: Load => 0
[192.168.1.20]: Status => Stopped
[192.168.1.20]: Load => 0
[192.168.1.30]: Status => Stopped
[192.168.1.30]: Load => 0
[192.168.1.60]: Status => Stopped
[192.168.1.60]: Load => 0
[192.168.1.70]: Status => Stopped
[192.168.1.70]: Load => 0
[192.168.1.80]: Load => 0
```
