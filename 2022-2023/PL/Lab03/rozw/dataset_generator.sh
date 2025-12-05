#!/bin/bash

for i in {1..50}
do
	LC_ALL=C tr -dc A-Za-z0-9 </dev/urandom | head -c 1000 > $(echo ${RANDOM} | md5sum | head -c 20).frame 
done