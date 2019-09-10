# Code style: 👇
https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions

# Branch rules: 🌿
There are two main branches - **master** and **dev**.  
👉**master** contains only changes that are stable and which will be shown during demo in class.  
  
👉**Dev** contains changes that are being developed in this week's sprint.   
  
👉If you want to start developing a new **feature**, then create a new branch from **dev**. Branch name should be: **feature/{feature-name}**, for example: **feature/login_screen**. After you finish developing it, then create a PR to dev branch. After successful merge, do not forget to **delete** the feature branch! (It can be done automatically on GitHub) 
  
👉If you want to fix a **bug**, then also create a branch from dev and naming should be: **bug/{bug-name}**  
  
After sprint, before showcasing the demo, dev branch should be merged to master! (But **do not** delete dev branch!!!)
  
# PR and Commit Rules:
Commit as frequently as possible. Avoid huge commits, with many code changes. Commit summary or description should include what was changed/added in the code.  
PR can be merged only after **at least two** teammates approved it.
