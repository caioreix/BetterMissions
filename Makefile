submodule-init:
	git submodule update --init

choco-setup:
	choco install dotnet-6.0-sdk

restore-release:
	@dotnet restore /p:Configuration=Release

restore-test:
	@dotnet restore /p:Configuration=Test

build-test: restore-test
	@dotnet build . --configuration Test --no-restore

build-release: restore-release
	@dotnet build . --configuration Release --no-restore

test-test: restore-test
	@dotnet test . --configuration Test --no-restore

test-release: restore-release
	@dotnet test . --configuration Release --no-restore

source:
	@dotnet nuget update source github -u $(GH_USER) -p $(GH_TOKEN) --store-password-in-clear-text
