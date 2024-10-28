# Negentropy.net

[![build](https://github.com/bezysoftware/negentropy.net/workflows/dotnet%20build/badge.svg)](https://github.com/bezysoftware/negentropy.net/actions)
[![latest version](https://img.shields.io/nuget/v/Negentropy.net)](https://www.nuget.org/packages/Negentropy.net)

.NET implementation of Negentropy Range-Based-Set-Reconciliation protocol. 

It's basically a binary search set-reconciliation algorithm. 
You can read about the details [here](https://logperiodic.com/rbsr.html). 
This code is basically a re-implementation of [Doug Hoyte's repository here](https://github.com/hoytech/negentropy)

## Installation

Either via Visual Studio [Nuget](https://www.nuget.org/packages/Negentropy.net) package manager, or from command line:

```powershell
dotnet add package Negentropy.net
```

The package can be used in `.NET 6` and newer.

## Usage

Your data items you want to reconcile need to implement `INegentropyItem` interface. 

```csharp
// use builder to create Negentropy instance
var builder = new NegentropyBuilder(new NegentropyOptions());
var negentropy = builder.AddRange(items).Build();

// client
var q = negentropy.Initiate();

// ...
// client x server communication
// ...

// reconciliation (both server and client)
var result = negentropy.Reconcile(q);

// algorithm terminates once result.Query is empty, otherwise repeat client x server back-and-forth

var whatIHaveThatServerDoesnt = result.HaveIds;
var whatServerHasThatIDont =  result.NeedIds;

```