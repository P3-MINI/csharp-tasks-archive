#if !STAGE1 && !STAGE2 && !STAGE3
Console.WriteLine("Uncomment lines defining STAGE constants in BreakingBad.props file to progress.");
Console.WriteLine("BreakingBad.props should be visible in your solution.");
#endif

#if STAGE1
Console.WriteLine("[Stage 1]\r\n\"Did you learn nothing from my chemistry class?\"\r\n");
Console.WriteLine(" After uncommenting STAGE1 in BreakingBad.props run tests for this stage with [Ctrl + R + A if in VS].");
Console.WriteLine();
#endif

#if STAGE2
Console.WriteLine("[Stage 2]\r\n\"And now, what shall we use to conduct this beautiful current with, hm? What one particular element comes to mind, hm?\"\r\n");
Console.WriteLine(" After uncommenting STAGE2 in BreakingBad.props run tests for this stage with [Ctrl + R + A if in VS].");
Console.WriteLine();
#endif

#if STAGE3
Console.WriteLine("[Stage 3]\r\n\"I did it for me. I liked it. I was good at it. And, I was really... I was alive.\"\r\n");
Console.WriteLine(" After uncommenting STAGE3 in BreakingBad.props run tests for this stage with [Ctrl + R + A if in VS].");
Console.WriteLine();
#endif