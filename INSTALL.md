# Installation Guide

This guide provides detailed instructions on how to install and set up the Janice framework.

## Prerequisites

Before you begin, ensure you have the following installed on your system:

- **Python 3.10 or higher**

You can check your Python version by running:

```bash
python3 --version
```

## Installation

1.  **Clone the repository:**

    ```bash
    git clone https://github.com/your-username/janice.git
    cd janice
    ```

2.  **Install the project and its dependencies:**

    The project uses `pyproject.toml` to manage dependencies. You can install everything using `pip`:

    ```bash
    pip install .
    ```

    This command installs the `janice` package in editable mode, so you can make changes to the source code and have them immediately reflected.

## Verifying the Installation

To ensure that the installation was successful, you can run the test suite:

```bash
pip install pytest
python3 -m pytest
```

All tests should pass.

## Running the Application

Once installed, you can use the `janice` command-line interface (CLI). To see the available commands, run:

```bash
janice --help
```
