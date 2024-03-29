#!/bin/bash

Help()
{
   echo "Take artifacts from docker container."
   echo
   echo "Syntax: copy_artifacts [-t|h|l]"
   echo "options:"
   echo "-t     Specifie the test type."
   echo "-l     Print a list a of test type."
   echo "-h     Help."
   echo 
}

PrintValidArtifactsList()
{   
    echo "Valid artifacts:"
    for ValidArtifacts in "${ValidArtifactsList[@]}"
    do
        echo "  - $ValidArtifacts"
    done
}

CheckList()
{
    Found=false
    for ValidArtifacts in "${ValidArtifactsList[@]}"
    do
        if [ "$ValidArtifacts" = "$Artifacts" ]; 
        then
            Found=true
            break
        fi
    done
    if [ "$Found" = false ]; 
    then
        echo -e "\033[0;31mThe artifact name is not valid. \nPlease check the list with valid artifacts.\033[0;31m"
    fi
}

MoveArtifacts()
{
    if [ "$Artifacts" = "unitTest" ]; 
    then
        echo "Moving unit test results"
        cp -r /src/BreakfastAPI.Test/TestResults /TestResults
        chmod 666 /src/BreakfastAPI.Test/TestResults
    fi
}

#-------
ValidArtifactsList=("unitTest" "altArtifact")
Artifacts=""

if [ $# -eq 0 ];
then
    Help
    Exit
fi

while getopts ":t:lh" opt; do
  case ${opt} in
    t)
      Artifacts=$OPTARG
      CheckList
      MoveArtifacts
      exit
      ;;
    l)
      PrintValidArtifactsList
      exit
      ;;
    h)
      Help
      exit
      ;;
    \? )
        Help
        exit;;
  esac
done
