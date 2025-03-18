#!/bin/bash
#
# To run feature tests in an isolated (actually containerized) environment.
#
# Copyright (c) 2023  Alexsys

APPDIR=`dirname "$0"`
APPNAME=`basename "$0"`
PROJECT_DIR=`realpath "$APPDIR/../.."`

function option_error() {
	echo "$@" >&2
	echo "Try '$APPNAME --help' for more information." >&2
	exit 2
}

function print_help() {
	cat <<EOF
Run feature tests of the project in a containerized environment.

Usage:
  $APPNAME [options]
  $APPNAME -h|--help

Options:
  -p, --project-name NAME     Specify a project name (default: project directory
                              name with a "_test" suffix)
EOF
}

while [ -n "$1" ]
do
	shift=1
	case "$1" in
	-h|--help)
		print_help
		exit 0
		;;
	-p|--project-name)
		PROJECT_NAME="$2"
		shift=2
		;;
	*)
		option_error "Invalid option '$1'"
		;;
	esac
	shift $shift
done

[ -n "$PROJECT_NAME" ] || PROJECT_NAME=$(basename "$PROJECT_DIR")_test

compose_args=(
	-p "$PROJECT_NAME"
	-f "$PROJECT_DIR/docker-compose.feature-tests.yml"
)

echo "Cleaning..."
docker-compose "${compose_args[@]}" down -v

if [ -x "$PROJECT_DIR/db/db-compose.sh" ]
then
	echo "Starting databases..."
	"$PROJECT_DIR/db/db-compose.sh" -p "$PROJECT_NAME" -t down
	"$PROJECT_DIR/db/db-compose.sh" -p "$PROJECT_NAME" -t up
fi

echo "Starting tests..."
docker-compose "${compose_args[@]}" pull
docker-compose "${compose_args[@]}" build --pull
docker-compose "${compose_args[@]}" up --no-start
container_id=$(docker-compose "${compose_args[@]}" ps -q api)
docker cp "$PROJECT_DIR/." ${container_id}:/src/target/
docker-compose "${compose_args[@]}" up

echo "Cleaning..."
docker cp ${container_id}:/src/target/tests/Bdd/TestResults/TEST.xml "$PROJECT_DIR/TEST.xml"
if [ -x "$PROJECT_DIR/db/db-compose.sh" ]
then
	docker-compose "${compose_args[@]}" rm -f
	"$PROJECT_DIR/db/db-compose.sh" -p "$PROJECT_NAME" -t down
else
	docker-compose "${compose_args[@]}" down
fi

exit 0
