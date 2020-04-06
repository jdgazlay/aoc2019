# Day 1

## Part 1 - Typescript

I wanted to write this in C# at first, but had some issue installing it on the computer I was working on at the moment, so doubled back to Typescript. I've been working in Typescript for a bit and figured this would be a good way to practice a bit more. So I completed part1 successfully.

## Part 2 - Javascript
This part proved a bit harder than I expected, because I wanted to use the [BigInt](https://tc39.es/ecma262/#sec-bigint-objects) feature in javascript to get the correct answer, but unfortunately it wasn't available in base Typescript & ts-node. BigInt was available in Node 10.4.0. So I converted the typescript to javascript and moved on from there. I was able to successfully get the answer with javascript BigInt AND without any integer overflows!

## C#
After I had ventured through all of the other stuff in TS & JS, I felt a little defeated and revisited my C# aspirations and was able to complete the entire day one module in C#.



Notes for me:
compile c# code
```bash
mcs -out:<output filename> <input filename>
```

Run C# code
```bash
mono <output filename from above>
```

Example:
```bash
mcs -out:part1.exe part1.cs
mono part1.exe
```
