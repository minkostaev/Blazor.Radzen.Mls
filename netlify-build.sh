#!/bin/sh

# List of branches to skip builds
#SKIP_BRANCHES="branch1 branch2 branch3"
SKIP_BRANCHES="publish"

# Check if the current branch is in the skip list
for BRANCH in $SKIP_BRANCHES; do
  if [ "$BRANCH" = "$HEAD" ]; then
    echo "Skipping build for branch $BRANCH"
    exit 0
  fi
done

# Default to not skipping the build
exit 1