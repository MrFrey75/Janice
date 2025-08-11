import pytest
from janice.proposer import propose, PROPOSAL_SCHEMA
import jsonschema

def test_proposal_valid():
    pr = propose("r1","test task", adversarial=False)
    jsonschema.validate(instance=pr, schema=PROPOSAL_SCHEMA)

def test_proposal_invalid_missing_steps():
    with pytest.raises(jsonschema.ValidationError):
        jsonschema.validate(instance={"adversarial":False}, schema=PROPOSAL_SCHEMA)
