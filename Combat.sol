// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

import "./BeastNFT.sol";

contract Combat {
    BeastNFT public beastNFT;

    constructor(address _beastNFT) {
        beastNFT = BeastNFT(_beastNFT);
    }

    function battle(uint256 attackerId, uint256 defenderId) external view returns (string memory) {
        require(beastNFT.ownerOf(attackerId) == msg.sender, "Not owner of attacker beast");

        BeastNFT.Beast memory attacker = beastNFT.getBeast(attackerId);
        BeastNFT.Beast memory defender = beastNFT.getBeast(defenderId);

        uint256 attackScore = attacker.attack + (attacker.speed > defender.speed ? 10 : 0);
        uint256 defenseScore = defender.defense + (defender.hp > attacker.hp ? 10 : 0);

        if (attackScore > defenseScore) {
            return "Attacker wins!";
        } else if (defenseScore > attackScore) {
            return "Defender wins!";
        } else {
            return "It's a draw!";
        }
    }
}
