# CPSC-362-proj
Each iteration will be in its own branch

## Goals
- Playtest
- Sound
  - Background music
  - Towers
  - Creeps
  - UI
- Creeps
  - Sprites
  - Special effects. Suggestions:
    - Passive Health regen
    - Spawn Creeps on Death
    - Movement speed modification
  - Random waves - Maybe
- Towers 
  - Sprites
  - New Types. Suggestions:
    - Splash
    - Slow
  - Projectiles - maybe
- UI
  - Refactor for Mobile
  - Improve tower purchasing
  - Display stage information
  - Zoom/Drag screen
  - Sprites
 
- Other 
  - Build for Android
  - Playtest
  - Organize notes
    - Include Global scope
  
 

## Important Folders
### Work Logs
 __OBSLETE__
Contains a brief description of the work each member has done.

### Class Meetings
Contains the agendas and notes for each in class meeting. 
This could technically be placed in documentation, but it is important enough to be placed in its own folder.

### Assets
Most of our actual work will be found in this folder. Subfolders will also have documentation on the actual scene itself.

### Others
Other folders are used by Unity3D.
We will likely not be opening these files at all.
  
 ## LINKS
 - Global Scope
   - https://docs.google.com/document/d/1HEtpih35UYRHMDWS0bYohscLYdZw9X2uXL7DZjVWBo0/edit?usp=sharing
 - Individual Contributions
   - https://docs.google.com/document/d/1zWxkH3BIcUR8q56eACtEO4s9zq3pXx23GSGU0DaI2mQ/edit?usp=sharing
 - Playtests feedback
   - https://docs.google.com/document/d/1ZcCCFNdJ9kXsTVaJg2jBFOskqH6nX29lRX3buvtwOI8/edit?usp=sharing
 - Trello
   - https://trello.com/b/HWrV0k1E/kanban-template

## AVOIDING MERGE CONFLICTS
  - Short living branches
    - Try to minimize the time between creating the branch and merging it back
  - Push and pull as often as possible
    - Tip: Use 'git fetch' to pull changes made by others into your local repo (You don't have to use 'git pull')
  - Don't always make you additions at the end of a file
    - Merge conflicts often arise when changes are made on the same line, this will minimize that possibility
  - When ever possible, use a new file
    - If everyone is working on new files, the only possible conflict is an identical file path 
    - (e.g. two people trying to upload a file named '/proj/file/foo.txt')
    
## HELPFUL GIT COMMANDS
  - git fetch
    - Get remote changes
  - git add .
    - Add all untracked files to your commit
  - git commit -m "<commit_msg>"
    - Commit changes with an inline commit message (This will skip the Vi editor)
  - git push
    - Push your changes to the current remote branch
  - git checkout <branch_name>
    - Switch your local branch
  - git checout -b <branch_name>
    - Switch your local branch to <Branch_name> or make a new branch with the same name if one does not exist
    - You must also run 'git push --set-upstream origin <branch_name>' to push to this new branch
  - git push origin --delete <branch_name>
    - Delete remote branch <branch_name>
  - git branch -d <branch_name>
    - Delete local branch, '-D' to force the deletion
  - git branch -a
    - List all local branches, if a branch you're looking for isn't here run 'git fetch'
  - git merge <branch_name>
    - Merges the specified branch into your CURRENT local branch, you must run 'git push' to save the merge on the remote repo
