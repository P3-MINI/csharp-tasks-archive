#if !STAGE1 && !STAGE2 && !STAGE3
Console.WriteLine("Uncomment lines defining STAGE constants in LosPollosHermanos.props file to progress.");
Console.WriteLine("LosPollosHermanos.props should be visible in your solution.");
#endif

#if STAGE1
Console.WriteLine("[Stage 1]\r\n\"You asked me if I was in the meth business or the money business. Neither. I’m in the chicken business.\"\r\n");
Console.WriteLine("After uncommenting STAGE1 in LosPollosHermanos.props run tests for this stage with [Ctrl + R + A if in VS].");
Console.WriteLine();
#endif

#if STAGE2
Console.WriteLine("[Stage 2]\r\n\"Yes, the old ways are still best at Los Pollos Hermanos. But don't take my word for it. One taste, and you'll know.\"\r\n");
Console.WriteLine("After uncommenting STAGE2 in LosPollosHermanos.props run tests for this stage with [Ctrl + R + A if in VS].");
Console.WriteLine();
#endif

#if STAGE3
Console.WriteLine("[Stage 3]\r\n\"I am delighted to report that the last quarter, Los Pollos Hermanos revenue has increased 8.3%.\"\r\n");
Console.WriteLine("After uncommenting STAGE3 in LosPollosHermanos.props run tests for this stage with [Ctrl + R + A if in VS].");
Console.WriteLine();
#endif
