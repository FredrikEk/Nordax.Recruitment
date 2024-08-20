import React, { useState } from "react";
import { Button } from '../common/button/Button';
import { Container } from 'reactstrap';
import LoanApplicationForm from "./LoanApplicationForm";
import LoanApplicationList from "./LoanApplicationList";

export const LoanApplication = () => {
    const [isOpen, setIsOpen] = useState<boolean>(false)

    const openLoanApplicationForm = () => {
        setIsOpen(!isOpen)
    }

    return (
        <Container>
            <div style={{ display: 'flex', justifyContent: 'flex-end' }}>
                <div style={{ display: 'flex', justifyContent: 'center', flexDirection: 'column', padding: '10px' }}>
                    {!isOpen ? (<h3 > Apply for Loan Application here!</h3>) : null}
                    <Button onClick={() => openLoanApplicationForm()} style={{
                        width: `${!isOpen ? '50%' : '100%'} `,
                        marginLeft: "5px",
                        marginRight: "5px",
                    }}>{!isOpen ? 'Go!' : 'Go Home'}</Button>
                </div>
            </div>
            {isOpen ? (
                <LoanApplicationForm />
            ) : <LoanApplicationList />
            }
        </Container >
    );
};